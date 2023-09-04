namespace RentApp.Models;

public class MongoDBSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string UserCollectionName { get; set; } = null!;
    public string OfficceCollectionName { get; set; } = null!;
    public string ApartmentCollectionName { get; set; } = null!;
    public string HouseCollectionName { get; set; } = null!;
}