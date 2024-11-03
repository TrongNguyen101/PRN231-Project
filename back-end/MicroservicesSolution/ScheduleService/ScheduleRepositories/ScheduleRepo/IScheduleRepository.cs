using ScheduleBusinessObject.Models;

namespace ScheduleRepositories.ScheduleRepo
{
    public interface IScheduleRepository
    {
        /// <summary>
        /// This function retrieves the schedule for each day
        /// </summary>
        /// <param name="day">The day for which the schedule is requested</param>
        /// <returns>A task representing th asynchronous operation.
        /// The task result contains the object for the given day or null if not found</returns>
        Task<Schedule?> GetScheduleByDay(DateTime day);
    }
}
