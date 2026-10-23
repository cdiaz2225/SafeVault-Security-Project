using NUnit.Framework;
using System.Net;

[TestFixture]
public class SafeVaultTests
    {
    [Test]
    public void TestForSQLInjection() 
    {
        // Arrange: Simular entrada maliciosa típica de SQL Injection
        string maliciousInput = "admin' OR '1'='1";

        // Act: Aplicar la función de sanitización
        string sanitizedOutput = InputValidator.SanitizeUsername(maliciousInput);

        // Assert: Comprobar que los caracteres de inyección fueron neutralizados
        Assert.IsFalse(sanitizedOutput.Contains("'"), "La entrada contiene comillas simples vulnerables a SQLi.");
        Assert.IsFalse(sanitizedOutput.Contains("="), "La entrada contiene signos de igualdad vulnerables a SQLi.");
    }

    [Test]
    public void TestForXSS() 
    {
        // Arrange: Simular entrada maliciosa con un script XSS
        string xssInput = "<script>alert('hack')</script>";

        // Act: Validar y desinfectar
        bool isValid = InputValidator.ValidateAndSanitizeEmail(xssInput, out string sanitizedOutput);

        // Assert: Verificar que el script XSS no pase y esté codificado
        Assert.IsFalse(isValid, "El script XSS pasó la validación de correo electrónico.");
        Assert.IsFalse(sanitizedOutput.Contains("<script>"), "El script malicioso no fue neutralizado correctamente.");
    }

    [Test]
    public void TestValidAuthentication()
    {
        // Act: Intentar autenticar con credenciales correctas
        bool isAuthenticated = AuthService.AuthenticateUser("regular_user", "UserPass123!", out string role);

        // Assert: Verificar autenticación exitosa y rol correcto
        Assert.IsTrue(isAuthenticated, "La autenticación debería ser exitosa para credenciales válidas.");
        Assert.AreEqual("user", role, "El rol asignado debe ser 'user'.");
    }

    [Test]
    public void TestInvalidAuthentication()
    {
        // Act: Intentar autenticar con contraseña incorrecta
        bool isAuthenticated = AuthService.AuthenticateUser("admin_user", "WrongPassword", out string role);

        // Assert: Verificar que el acceso sea denegado
        Assert.IsFalse(isAuthenticated, "La autenticación debe fallar con una contraseña incorrecta.");
        Assert.IsEmpty(role, "No debe asignarse ningún rol si la autenticación falla.");
    }

    [Test]
    public void TestAdminAuthorizationSuccess()
    {
        // Act & Assert: Un usuario con rol admin intenta acceder al panel
        string result = AuthorizationService.AccessAdminPanel("admin");
        Assert.IsNotNull(result, "El administrador debe poder acceder al panel.");
    }

    [Test]
    public void TestAdminAuthorizationFailure()
    {
        // Act & Assert: Un usuario estándar intenta acceder al panel de administración
        Assert.Throws<UnauthorizedAccessException>(() => 
        {
            AuthorizationService.AccessAdminPanel("user");
        }, "Un usuario regular no debe tener acceso al panel de administración.");
    }
}
