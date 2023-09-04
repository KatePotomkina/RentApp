using System.Collections.Specialized;
using RentApp.Collection;

namespace RentApp.Repository;

public interface IRealEstateRepository
{
    Task<List<Apartament>> GetAllApartmentsAsync();
    Task<List<RealEstate>> GetAllPropertiesAsync();
    Task<List<Office>> GeyAllOfficesAsync();
    Task<List<House>> GeyAllHousesAsync();
    Task<Apartament> GetByIdApartaments(string id);
    Task CreateAsync(Apartament newApartment);
    Task RemoveAsync(string id);
    Task UpdateAsync(string id, Apartament updatedApartment);
    Task<House> GetByIdHouses(string id);
    Task CreateHouseAsync(House newHouse);
    Task RemoveHouseAsync(string id);
    Task UpdateHouseAsync(string id, House updatedHouse);
    Task UpdateOfficeAsync(string id, Office updatedOffice);
    Task RemoveOfficeAsync(string id);
    Task CreateOfficeAsync(Office newOffice);
    Task<Office> GetByIdOffice(string id);
}