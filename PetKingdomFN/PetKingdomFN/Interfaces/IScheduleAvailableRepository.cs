using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IScheduleAvailableRepository
    {
        Task<DataList<ScheduleAvailable>> GetPageList(Pagination page);
        Task<DataList<ScheduleAvailable>> SearchScheduleAvailable(Pagination page, basedSearchObject searchObj);
        Task<ScheduleAvailable> GetScheduleAvailableById(string id);
        Task<ScheduleAvailable> AddScheduleAvailable(ScheduleAvailable sa);
        Task<ScheduleAvailable> UpdateScheduleAvailable(ScheduleAvailable sa);
        Task<int> DeleteScheduleAvailable(string id);
    }
}
