using Microsoft.Data.SqlClient;

namespace Models
{
    // Class to read data from the database
    public class DataReader
    {
        // READ DATA FROM TABLE
        public void ReadData(string _connectionString)
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
                    Console.Write("Choose a table to read data from (or press Enter to skip): ");
                    string input = Console.ReadLine() ?? string.Empty;
                    Console.Clear();
                    if (int.TryParse(input, out int choice) && choice >= 1 && choice <= tableNames.Count)
                    {
                        using (SqlCommand command = new($"SELECT * FROM {tableNames[choice - 1]}", connection))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<int> columnWidths = new();
                            int fieldCount = reader.FieldCount;

                            for (int i = 0; i < fieldCount; i++)
                            {
                                int maxLength = reader.GetName(i).Length;
                                columnWidths.Add(maxLength);
                            }

                            List<string[]> rows = new();
                            while (reader.Read())
                            {
                                string[] row = new string[fieldCount];
                                for (int i = 0; i < fieldCount; i++)
                                {
                                    string value = reader[i]?.ToString() ?? string.Empty;
                                    columnWidths[i] = Math.Max(columnWidths[i], value.Length);
                                    row[i] = value;
                                }
                                rows.Add(row);
                            }

                            for (int i = 0; i < fieldCount; i++)
                            {
                                Console.Write(reader.GetName(i).PadRight(columnWidths[i] + 2));
                            }
                            Console.WriteLine();

                            foreach (var row in rows)
                            {
                                for (int i = 0; i < fieldCount; i++)
                                {
                                    Console.Write(row[i].PadRight(columnWidths[i] + 2));
                                }
                                Console.WriteLine();
                            }
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}