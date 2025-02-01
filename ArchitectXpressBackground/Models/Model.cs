using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ArchitectXpress.Models;

public class ProfileKoli
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Occupation { get; set; }

    public ProfileKoli(string name, int age, string address, string phoneNumber, string email, string occupation)
    {
        Name = name;
        Age = age;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Occupation = occupation;
    }
}

