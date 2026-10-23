public class AuthorizationService
{
    // Verifica si un usuario con un rol específico tiene permiso para acceder a una ruta protegida
    public static bool AuthorizeAccess(string userRole, string requiredRole)
    {
        if (string.IsNullOrEmpty(userRole))
        {
            return false;
        }

        // Validación de rol (ej. comparar si el rol del usuario coincide con el requerido)
        return userRole.Equals(requiredRole, StringComparison.OrdinalIgnoreCase);
    }

    // Ejemplo de método protegido para el panel de administración
    public static string AccessAdminPanel(string userRole)
    {
        if (AuthorizeAccess(userRole, "admin"))
        {
            return "Acceso concedido al panel de administración de SafeVault.";
        }
        throw new UnauthorizedAccessException("Acceso denegado: Se requieren privilegios de administrador.");
    }
}
