using System.Text.RegularExpressions;
using System.Net;

public class InputValidator
{
    // Valida y desinfecta el nombre de usuario (solo permite caracteres alfanuméricos y guiones bajos)
    public static string SanitizeUsername(string username)
    {
        if (string.IsNullOrEmpty(username)) return string.Empty;
        
        // Eliminar cualquier carácter que no sea alfanumérico o guion bajo
        return Regex.Replace(username, "[^a-zA-Z0-9_]", string.Empty);
    }

    // Valida el formato del correo electrónico y neutraliza posibles scripts
    public static bool ValidateAndSanitizeEmail(string email, out string sanitizedEmail)
    {
        sanitizedEmail = string.Empty;
        if (string.IsNullOrEmpty(email)) return false;

        // Codificar para prevenir XSS básico si se renderiza en HTML
        string cleanEmail = WebUtility.HtmlEncode(email.Trim());

        // Expresión regular estándar para validar correo electrónico
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (Regex.IsMatch(cleanEmail, emailPattern, RegexOptions.IgnoreCase))
        {
            sanitizedEmail = cleanEmail;
            return true;
        }
        return false;
    }
}
