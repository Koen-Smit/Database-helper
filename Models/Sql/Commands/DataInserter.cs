using Microsoft.Data.SqlClient;

namespace Models
{
    // Class to insert data to the database
    public class DataInserter
    {      
        // INSERT DATA INTO TABLE
        public void InsertData(string _connectionString)
        {
            using (SqlConnection connection = new(_connectionString)){
                connection.Open();
                Console.WriteLine("Connection is open");
                List<string> MangaNames = new(){"Bleach", "Dragon Ball", "Attack on Titan", "My Hero Academia", "Death Note", "Tokyo Ghoul",};
                using(SqlCommand command = connection.CreateCommand()){
                    foreach (string mangaName in MangaNames)
                    {
                        command.CommandText = $"Insert into mangaComics(Name, Volume, IsRead) values('{mangaName}', {Random.Shared.Next(1, 10)}, {Random.Shared.Next(0, 1)})";
                        // command.AddParameter("Name", mangaName);
                        command.ExecuteNonQuery();
                    }
                }
                using(SqlCommand command = connection.CreateCommand()){
                    command.CommandText = "Select * from mangaComics";
                    using(SqlDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                            Console.WriteLine($"Name: {reader["Name"]}, Volume: {reader["Volume"]}, IsRead: {reader["IsRead"]}");
                        }
                    }
                }
                Console.WriteLine(connection.State);

                connection.Close();
                Console.WriteLine("Connection is closed");
            }
        }
    }
}