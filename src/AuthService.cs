using BCrypt.Net;

public class AuthService
{
    // Simulación de base de datos de usuarios (en producción vendría de un repositorio)
    private static readonly Dictionary<string, (string PasswordHash, string Role)> _users = new()
    {
        { "admin_user", (BCrypt.Net.BCrypt.HashPassword("AdminSecuro123!"), "admin") },
        { "regular_user", (BCrypt.Net.BCrypt.HashPassword("UserPass123!"), "user") }
    };

    // Función para autenticar usuarios comparando contraseñas con hash
    public static bool AuthenticateUser(string username, string password, out string role)
    {
        role = string.Empty;

        if (_users.TryGetValue(username, out var userInfo))
        {
            // Verificar la contraseña ingresada contra el hash almacenado usando BCrypt
            bool isValid = BCrypt.Net.BCrypt.Verify(password, userInfo.PasswordHash);
            if (isValid)
            {
                role = userInfo.Role;
                return true;
            }
        }
        return false;
    }
}
