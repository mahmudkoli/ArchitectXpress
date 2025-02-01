using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ArchitectXpress.Models;

[BsonIgnoreExtraElements]
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
public class Resume
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public ContactInfo Contact { get; set; }

    public List<Education> Education { get; set; }

    public List<Skill> Skills { get; set; }

    public List<Experience> Experience { get; set; }

    public List<string> Certifications { get; set; }

    public List<string> Languages { get; set; }

    public List<string> Hobbies { get; set; }

    public List<string> VolunteerExperience { get; set; }

    public List<string> References { get; set; }

    public List<string> Publications { get; set; }

    public List<string> Patents { get; set; }

    public List<string> Awards { get; set; }

    [BsonElement("TextSearchString")]
    public string TextSearchString { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DateOfBirth { get; set; }

    public double RandomRating { get; set; }

    public int YearsSinceProfileCreation { get; set; }
}

public class ContactInfo
{
    public string Email { get; set; }
    public string Phone { get; set; }
}

public class Education
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int? Ranking { get; set; }
    public string Type { get; set; }
    public string Field { get; set; }
    public string Honors { get; set; }
}

public class Skill
{
    public string Name { get; set; }
    public string Proficiency { get; set; }
    public string Experience { get; set; }
}

public class Experience
{
    public CompanyInfo Company { get; set; }
    public string Position { get; set; }
    public List<Project> Projects { get; set; }
}

public class CompanyInfo
{
    public string Name { get; set; }
    public string Industry { get; set; }
    public string Location { get; set; }
}

public class Project
{
    public string ProjectName { get; set; }
    public List<Technology> Technologies { get; set; }
    public string Duration { get; set; }
}

public class Technology
{
    public string Name { get; set; }
    public string Version { get; set; }
}
