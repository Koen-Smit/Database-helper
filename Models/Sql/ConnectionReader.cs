using Microsoft.Extensions.Configuration;

namespace Models
{
    public class ConnectionReader
    {
        private IConfiguration config;
        private readonly string connectionString;

        public ConnectionReader()
        {
            // Use the full path to the configuration file
            string projectRoot = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName 
                ?? throw new InvalidOperationException("Could not determine project root directory.");

            string configPath = Path.Combine(projectRoot, "Assets", "Secret", "database.json");

            if (!File.Exists(configPath))
                throw new FileNotFoundException($"The configuration file was not found at path: {configPath}");

            config = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found in configuration.");
        }

        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}
