using Microsoft.Data.SqlClient;

namespace Models
{
    public class SqlCommands
    {
        private readonly string _connectionString;

        public SqlCommands(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Executes an SQL file
        public void ExecuteSqlFile()
        {
            var sqlExecuter = new SqlExecuter();
            sqlExecuter.ExecuteSqlFile(_connectionString);
        }

        // Inserts data into the database
        public void InsertData()
        {
            var dataInserter = new DataInserter();
            dataInserter.InsertData(_connectionString);
        }

        // Reads data from the database
        public void ReadData()
        {
            var dataReader = new DataReader();
            dataReader.ReadData(_connectionString);
        }

        // Deletes a table from the database
        public void DeleteTable()
        {
            var tableDeleter = new TableDeleter();
            tableDeleter.DeleteTable(_connectionString);
        }

        // Exits the program
        public void Exit()
        {
            Console.WriteLine("Exiting the program...");
        }
    }
}
