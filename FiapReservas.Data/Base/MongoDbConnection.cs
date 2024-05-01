using MongoDB.Driver;

namespace FiapReservas.Data.Base
{
    public class MongoDbConnection
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        private readonly string _database = Environment.GetEnvironmentVariable("Database");

        protected IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_connectionString);
            return client.GetDatabase(_database);
        }
    }
}
