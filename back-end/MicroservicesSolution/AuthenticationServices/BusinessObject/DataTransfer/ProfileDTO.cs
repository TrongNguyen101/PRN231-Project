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
        public DateOnly Birthday { get; set; }
        public int MajorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public AccountDTO? Account { get; set; }
    }
}