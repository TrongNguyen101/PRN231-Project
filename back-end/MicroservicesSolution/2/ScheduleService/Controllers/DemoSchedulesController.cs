using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleBusinessObject.DataContext;
using ScheduleBusinessObject.Models;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoSchedulesController : ControllerBase
    {
        private readonly ScheduleContext _context;

        public DemoSchedulesController(ScheduleContext context)
        {
            _context = context;
        }

        // GET: api/DemoSchedules
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<object>>> GetSchedules()
        {
            var result = new List<object>();

            // Lặp qua các slot từ 1 đến 6
            for (int slot = 1; slot <= 4; slot++)
            {
                var slotData = new Dictionary<string, object>
                    {
                        { "slot", $"Slot {slot}" },
                        { "monday", null },
                        { "tuesday", null },
                        { "wednesday", null },
                        { "thursday", null },
                        { "friday", null },
                        { "saturday", null },
                        { "sunday", null }
                    };


                var schedules = _context.Schedules
                   .Include(s => s.Subject)
                   .Include(s => s.TimeSlot)
                   .Include(s => s.Room)// Nếu có bảng liên quan, ví dụ TimeSlot
                   .Where(s => s.TimeSlotId == slot)
                   .Select(s => new { s.Date.DayOfWeek, Schedule = s })
                   .ToList()
                   .GroupBy(s => s.DayOfWeek)
                   .ToList();

                // Duyệt qua từng nhóm (ngày trong tuần)
                foreach (var scheduleGroup in schedules)
                {
                    var day = scheduleGroup.Key switch
                    {
                        DayOfWeek.Monday => "monday",
                        DayOfWeek.Tuesday => "tuesday",
                        DayOfWeek.Wednesday => "wednesday",
                        DayOfWeek.Thursday => "thursday",
                        DayOfWeek.Friday => "friday",
                        DayOfWeek.Saturday => "saturday",
                        DayOfWeek.Sunday => "sunday",
                        _ => null
                    };

                    if (day != null)
                    {
                        // Chỉ lấy lịch đầu tiên trong nhóm (hoặc có thể xử lý theo yêu cầu của bạn)
                        var schedule = scheduleGroup.First();
                        slotData[day] = new
                        {
                            subject = schedule.Schedule.Subject.SubjectName ,
                            time = $"{schedule.Schedule.TimeSlot.TimeStart} - {schedule.Schedule.TimeSlot.TimeEnd}",
                            room = schedule.Schedule.Room.RoomName,
                            date = schedule.Schedule.Date
                        };
                    }
                }

                result.Add(slotData);
            }

            return Ok(result);
        }

        [HttpGet("by-class-and-date")]
        public async Task<IActionResult> GetSchedulesByClassAndDate([FromQuery] int classId, [FromQuery] string date)
        {
            // Kiểm tra đầu vào của ngày có hợp lệ không
            if (!DateOnly.TryParse(date, out DateOnly searchDate))
            {
                return BadRequest("Invalid date format. Please use yyyy-MM-dd.");
            }

            // Thực hiện truy vấn để lấy các bản ghi Schedule theo classId và ngày
            var schedules = await _context.Schedules
                   .Where(s => s.ClassId == classId && s.Date == searchDate)
                   .Select(s => new
                   {
                       SlotName = s.TimeSlot.SlotId,
                       SubjectName = s.Subject.SubjectName,
                       Time = $"({s.TimeSlot.TimeStart} - {s.TimeSlot.TimeEnd})",
                       ClassName = s.Class.ClassName,
                       Room = s.Room.RoomName
                   })
                     .ToListAsync();

            // Kiểm tra nếu không có bản ghi nào
            if (schedules == null || !schedules.Any())
            {
                return NotFound("No schedules found for the given class and date.");
            }

            return Ok(schedules);
        }


        // GET: api/DemoSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        // PUT: api/DemoSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DemoSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSchedule([FromBody] Schedule schedule)
        {

            // Kiểm tra trùng lịch theo từng tiêu chí
            bool isRoomOccupied = _context.Schedules.Any(s => s.RoomId == schedule.RoomId &&
                                                              s.TimeSlotId == schedule.TimeSlotId &&
                                                              s.Date == schedule.Date);

            if (isRoomOccupied)
            {
                return BadRequest("The room is not available");
            }

            bool isLecturerOccupied = _context.Schedules.Any(s => s.LectureId == schedule.LectureId &&
                                                                  s.TimeSlotId == schedule.TimeSlotId &&
                                                                  s.Date == schedule.Date);
            if (isLecturerOccupied)
            {
                return BadRequest("The Lecturer is busy");
            }

            bool isClassScheduled = _context.Schedules.Any(s => s.ClassId == schedule.ClassId &&
                                                                s.TimeSlotId == schedule.TimeSlotId &&
                                                                s.Date == schedule.Date);

            if (isClassScheduled)
            {
                return BadRequest("The class had another schedule");
            }

            // Ràng buộc số lượng giảng viên tối đa
            //int lecturerCount = _context.Lecturers.Count();
            int schedulesAtSameTime = _context.Schedules.Count(s => s.TimeSlotId == schedule.TimeSlotId &&
                                                                    s.Date == schedule.Date);
            //if (schedulesAtSameTime >= lecturerCount)
            //{
            //    return BadRequest("Số lượng lịch tại cùng thời gian đã đạt tối đa cho số giảng viên khả dụng.");
            //}


          

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return Ok("The class added successfully.");
        }

        // DELETE: api/DemoSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }
    }
}
