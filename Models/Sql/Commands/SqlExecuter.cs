using Microsoft.Data.SqlClient;

namespace Models
{
    // Class to run an sql file
    public class SqlExecuter
    {    
        // CHOOSE/EXECUTE SQL FILE
        public void ExecuteSqlFile(string _connectionString)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sqlFolderPath = Path.Combine(projectDirectory, @"..\..\..\Assets\Sql");

            // Get all SQL files from the "sql" folder
            string[] sqlFiles = Directory.GetFiles(sqlFolderPath, "*.sql");

            if (sqlFiles.Length == 0)
            {
                Console.WriteLine("No SQL files found in the 'sql' folder.");
                return;
            }

            // Display the list of SQL files to the user
            Console.WriteLine("Select an SQL file to execute:");
            for (int i = 0; i < sqlFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(sqlFiles[i])}");
            }

            // Get the user's selection
            int selectedFileIndex;
            do
            {
                Console.Write("Enter the number of the file you want to execute: ");
            } while (!int.TryParse(Console.ReadLine(), out selectedFileIndex) || selectedFileIndex < 1 || selectedFileIndex > sqlFiles.Length);

            // Extract the table name from the file name (without the .sql extension)
            string selectedFileName = Path.GetFileNameWithoutExtension(sqlFiles[selectedFileIndex - 1]);

            // Check if the table already exists in the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                Console.WriteLine("Database connection is open.");

                bool tableExists;
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
                    command.Parameters.AddWithValue("@TableName", selectedFileName);
                    tableExists = (int)command.ExecuteScalar() > 0;
                }

                if (tableExists)
                {
                    Console.WriteLine($"Table '{selectedFileName}' already exists. Skipping execution of {selectedFileName}.sql.");
                    connection.Close();
                    Console.WriteLine("Database connection is closed.");
                    return;
                }

                // Read the selected SQL file
                string sql = File.ReadAllText(sqlFiles[selectedFileIndex - 1]);
                Console.WriteLine($"\nExecuting SQL file: {Path.GetFileName(sqlFiles[selectedFileIndex - 1])}\n");

                // Execute the SQL script
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("SQL script executed successfully.");
                connection.Close();
                Console.WriteLine("Database connection is closed.");
            }
        }
    }
} 