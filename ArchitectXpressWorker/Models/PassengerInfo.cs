using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ArchitectXpress.Models;

[BsonIgnoreExtraElements]
public class PassengerInfo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string PassnegerId { get; set; }
    public string Name { get; set; }
    public int Discount { get; set; }

    public PassengerInfo()
    {

    }

    public PassengerInfo(string passnegerId, string name, int discount)
    {
        Name = name;
        Discount = discount;
        PassnegerId = passnegerId;
    }
}