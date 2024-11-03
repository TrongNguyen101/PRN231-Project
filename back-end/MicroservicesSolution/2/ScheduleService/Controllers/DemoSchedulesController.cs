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
            for (int slot = 1; slot <= 6; slot++)
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
                            //online = schedule.Schedule.TimeSlot.Status
                        };
                    }
                }

                result.Add(slotData);
            }

            return Ok(result);
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
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchedule", new { id = schedule.ScheduleId }, schedule);
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
