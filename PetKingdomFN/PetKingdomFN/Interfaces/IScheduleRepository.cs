using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IScheduleRepository
    {
        Task<DataList<Schedule>> GetPageList(Pagination page);
        Task<DataList<Schedule>> SearchSchedule(Pagination page, basedSearchObject searchObj);
        Task<Schedule> GetScheduleById(string id);
        Task<Schedule> AddSchedule(Schedule schedule);
        Task<Schedule> UpdateSchedule(Schedule schedule);
        Task<int> DeleteSchedule(string id);
    }
}
