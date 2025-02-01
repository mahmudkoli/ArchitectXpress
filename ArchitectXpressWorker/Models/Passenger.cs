using MongoDB.Bson;

namespace Archexpress.Demo.Passenger.Database
{
    [Serializable]
    public class Passenger
    {
        public ObjectId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public List<string> IdentificationDocuments { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Status { get; set; }
        public double? Rating { get; set; }
        public string ReferenceId { get; set; }
        public long CreatedAt { get; set; }
        public long UpdatedAt { get; set; }
        public long? DeletedAt { get; set; }
    }
}
