using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using RentApp.Collection;
using RentApp.Models;

namespace RentApp.Repository;

public class RealEstateRepository : IRealEstateRepository
{
    // private readonly IMongoCollection<RealEstate> _realestate;
    private readonly IMongoCollection<Apartament> _apartmentproperties;
    private readonly IMongoCollection<House> _houseproperties;
    private readonly IMongoCollection<Office> _officeProperties;

    public RealEstateRepository(IOptions<MongoDBSettings> mongoDatabaseSettings)
    {
        var mongoClient = new MongoClient(mongoDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

        _apartmentproperties =
            mongoDatabase.GetCollection<Apartament>(mongoDatabaseSettings.Value.ApartmentCollectionName);
        _houseproperties = mongoDatabase.GetCollection<House>(mongoDatabaseSettings.Value.HouseCollectionName);
        _officeProperties = mongoDatabase.GetCollection<Office>(mongoDatabaseSettings.Value.OfficceCollectionName);
    }

    public async Task<List<RealEstate>> GetAllPropertiesAsync()
    {
        var apartamentsDocuments = await _apartmentproperties.Find(_ => true).ToListAsync();
        var officesDocuments = await _officeProperties.Find(_ => true).ToListAsync();
        var housesDocuments = await _houseproperties.Find(_ => true).ToListAsync();

        var allProperties = new List<RealEstate>();
        allProperties.AddRange(apartamentsDocuments);
        allProperties.AddRange(officesDocuments);
        allProperties.AddRange(housesDocuments);
        return allProperties;
    }

    public async Task<List<Office>> GeyAllOfficesAsync()
    {
        return await _officeProperties.Find(_ => true).ToListAsync();
    }

    public async Task<List<House>> GeyAllHousesAsync()
    {
        var housesDocuments = await _houseproperties.Find(_ => true).ToListAsync();
        return housesDocuments;
    }


    public async Task<List<Apartament>> GetAllApartmentsAsync()
    {
        return await _apartmentproperties.Find(_ => true).ToListAsync();
    }

    public async Task<Apartament> GetByIdApartaments(string id)
    {
        return await _apartmentproperties.Find(p => id == p.Id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Apartament newapartment)
    {
        newapartment.Id = ObjectId.GenerateNewId().ToString();
        await _apartmentproperties.InsertOneAsync(newapartment);
    }

    public async Task UpdateAsync(string id, Apartament updatedApartment)
    {
        await _apartmentproperties.ReplaceOneAsync(x => x.Id == id, updatedApartment);
    }

    public async Task RemoveAsync(string id)
    {
        await _apartmentproperties.DeleteOneAsync(x => x.Id == id);
    }


    public async Task<House> GetByIdHouses(string id)
    {
        return await _houseproperties.Find(p => id == p.Id).FirstOrDefaultAsync();
    }

    public async Task CreateHouseAsync(House newHouse)
    {
        newHouse.Id = ObjectId.GenerateNewId().ToString();
        await _houseproperties.InsertOneAsync(newHouse);
    }

    public async Task RemoveHouseAsync(string id)
    {
        await _houseproperties.DeleteOneAsync(x => x.Id == id);
    }

    public async Task UpdateHouseAsync(string id, House updatedHouse)
    {
        await _houseproperties.ReplaceOneAsync(x => x.Id == id, updatedHouse);
    }


    public async Task<Office> GetByIdOffice(string id)
    {
        return await _officeProperties.Find(p => id == p.Id).FirstOrDefaultAsync();
    }

    public async Task CreateOfficeAsync(Office newOffice)
    {
        newOffice.Id = ObjectId.GenerateNewId().ToString();
        await _officeProperties.InsertOneAsync(newOffice);
    }

    public async Task RemoveOfficeAsync(string id)
    {
        await _officeProperties.DeleteOneAsync(x => x.Id == id);
    }

    public async Task UpdateOfficeAsync(string id, Office updatedOffice)
    {
        await _officeProperties.ReplaceOneAsync(x => x.Id == id, updatedOffice);
    }
}