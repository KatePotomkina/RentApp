using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RentApp.Collection;

public abstract class RealEstate
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")] public string? Name { get; set; } = null!;

    [BsonElement("Address")] public string? Address { get; set; } = null!;

    [BsonElement("Price")] public double Price { get; set; }

    [BsonElement("Size")] public double Size { get; set; }

    [BsonElement("Square")] public int Square { get; set; }

    [BsonElement("Floor")] public int Floor { get; set; }

    [BsonElement("Type")] public string? Type { get; set; }= null!;

    [BsonElement("SubObjectId")] public ObjectId SubObjectId { get; set; }
}