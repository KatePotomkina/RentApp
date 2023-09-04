using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RentApp.Collection;

public class People : MongoIdentityUser<Guid>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")] public string Name { get; set; } = null!;

    [BsonElement("Email")] public string Email { get; set; } = null!;
    [BsonElement("Password")] public string Password { get; set; }
    [BsonElement("Role")] public string Role { get; set; }

    [BsonElement("UserName")] public string UserName { get; set; }
    [BsonElement("Adverts")] public List<RealEstate> Adverts { get; set; }
}