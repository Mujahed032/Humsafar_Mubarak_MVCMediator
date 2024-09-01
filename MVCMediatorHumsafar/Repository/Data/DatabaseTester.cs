using Microsoft.Data.SqlClient;

public class DatabaseTester
{
    private readonly string _connectionString;

    public DatabaseTester(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void TestConnection()
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection to the database was successful!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}