namespace ScheduleBusinessObject.Models
{
    public class StudentInClass
    {
        public string StudentId { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
