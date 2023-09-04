using System.Collections.Specialized;
using System.Globalization;
using RentApp.Collection;

namespace RentApp.Repository;

public interface IPeopleRepository
{
    Task<List<People>> GetAllAsync();
    Task<People> GetAsync(string id);
    Task CreateAsync(People newperson);
    Task UpdateAsync(string id, People updatedPerson);
    Task RemoveAsync(string id);
}