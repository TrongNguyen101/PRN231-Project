using System.ComponentModel.DataAnnotations;

namespace ScheduleBusinessObject.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int NumberOfSlot { get; set; } = 2;
        public ICollection<Schedule> Schedules { get; set; }
    }
}