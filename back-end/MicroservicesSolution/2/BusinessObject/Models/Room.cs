using System.ComponentModel.DataAnnotations;

namespace ScheduleBusinessObject.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public bool Status { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}