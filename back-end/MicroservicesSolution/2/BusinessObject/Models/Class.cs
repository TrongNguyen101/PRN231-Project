using System.ComponentModel.DataAnnotations;

namespace ScheduleBusinessObject.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<StudentInClass> StudentsInClass { get; set; }
    }
}