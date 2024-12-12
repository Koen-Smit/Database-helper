using Microsoft.Data.SqlClient;

namespace Models
{
    // Class to delete a table from the database
    public class TableDeleter
    {
        // CHOOSE/DELETE TABLE
        public void DeleteTable(string _connectionString)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                List<string> tableNames = new();
                using (SqlCommand command = new("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
                if (tableNames.Count > 0)
                {
                    Console.WriteLine("Available tables:");
                    for (int i = 0; i < tableNames.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {tableNames[i]}");
                    }
                    Console.Write("Choose a table to delete (or press Enter to skip): ");
                    string input = Console.ReadLine() ?? string.Empty;
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= tableNames.Count)
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = $"DROP TABLE {tableNames[choice - 1]}";
                            command.ExecuteNonQuery();
                            Console.WriteLine($"Table {tableNames[choice - 1]} deleted.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No existing tables found in the database.");
                }
                connection.Close();
            }
        }
    }
}
    