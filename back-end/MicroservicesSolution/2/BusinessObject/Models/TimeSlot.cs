using System.ComponentModel.DataAnnotations;

namespace ScheduleBusinessObject.Models
{
    public class TimeSlot
    {
        [Key]
        public int SlotId { get; set;}
        public TimeOnly TimeStart { get; set;}
        public TimeOnly TimeEnd { get; set;}
        public bool Status { get; set;}
        public ICollection<Schedule> Schedules { get; set; }
    }
}