using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RentApp.Collection;
using RentApp.Models;

namespace RentApp.Repository;

public class PeopleRepository : IPeopleRepository
{
    private readonly IMongoCollection<People> _peopleCollection;

    public PeopleRepository(IOptions<MongoDBSettings> mongoDatabaseSettings)
    {
        var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

        _peopleCollection = mongoDatabase.GetCollection<People>(mongoDatabaseSettings.Value.UserCollectionName);
    }

    public async Task<List<People>> GetAllAsync()
    {
        return await _peopleCollection.Find(_ => true).ToListAsync();
    }

    public async Task<People> GetAsync(string id)
    {
        return await _peopleCollection.Find(_ => _.Id == id).FirstOrDefaultAsync();
    }


    public async Task CreateAsync(People newpeople)
    {
        await _peopleCollection.InsertOneAsync(newpeople);
    }

    public async Task UpdateAsync(string id, People updatedPerson)
    {
        await _peopleCollection.ReplaceOneAsync(x => x.Id == id, updatedPerson);
    }

    public async Task RemoveAsync(string id)
    {
        await _peopleCollection.DeleteOneAsync(x => x.Id == id);
    }
}