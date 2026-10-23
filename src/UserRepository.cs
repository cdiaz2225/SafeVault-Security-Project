using System.Data.SqlClient; // O el proveedor de base de datos correspondiente (ej. MySql.Data.MySqlClient)

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Método seguro usando consultas parametrizadas
    public void InsertUser(string username, string email)
    {
        string query = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Uso de parámetros para prevenir inyección SQL
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
