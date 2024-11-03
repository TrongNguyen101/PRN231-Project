using ScheduleBusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleRepositories.ScheduleRepo
{
    public class ScheduleRepository : IScheduleRepository
    {
        public Task<Schedule?> GetScheduleByDay(DateTime dateTime) =>  
    }
}
