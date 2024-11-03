using System.ComponentModel.DataAnnotations;

namespace ScheduleBusinessObject.Models
{
    public class Schedule
    {
        //unique identifier of each schedule
        [Key]
        public int ScheduleId { get; set; }

        //Foreign key in class table
        public int ClassId { get; set; }
        //Navigation property - A class has one schedule at the time
        public Class? Class { get; set; }

        //Foreign key in lecture table
        public string LectureId { get; set; }

        //Foreign key in room table
        public int RoomId { get; set; }
        //Navigation property - A room has one schedule at the time
        public Room? Room { get; set; }

        //Foreign key in subject table
        public int SubjectId { get; set; }
        //Navigation property - A subject has one schedule which depend on the number of students, with one teacher, at a specific time and held in one room 
        public Subject? Subject { get; set; }

        //Foreign key in slot time table
        public int TimeSlotId { get; set; }
        //Navigation property - A slot time has one schedule which depend on the number of students, with one teacher, and held in one room 
        public TimeSlot? TimeSlot { get; set; }

        //Everyday has schedule 
        public DateOnly Date { get; set; }

        public string? Isdelete { get; set; }
    }
}