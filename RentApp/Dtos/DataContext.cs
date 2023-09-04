using MongoDB.Driver;
using RentApp.Collection;

namespace RentApp.Dtos;



public class DataContext
{
    private readonly IMongoDatabase _database;

    public DataContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<People> Users => _database.GetCollection<People>("Users");
}

