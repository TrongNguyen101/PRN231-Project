using BusinessObject.Models;

namespace BusinessObject.DataTransfer
{
    public class ProfileDTO
    {
        public Guid ProfileId { get; set; }
        public int? AccountId { get; set; }
        public string? Code { get; set; }
        public string? FirtName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int GenderId { get; set; }
        public string Birthday { get; set; }
        public MajorDTO? MajorDTO { get; set; }
    }
}