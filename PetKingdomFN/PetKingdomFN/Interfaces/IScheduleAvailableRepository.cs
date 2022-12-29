using Microsoft.CodeAnalysis.Options;
using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IScheduleAvailableRepository
    {
        Task<DataList<ScheduleAvailable>> GetPageList(Pagination page,string optionId);
        Task<DataList<ScheduleAvailable>> SearchScheduleAvailable(Pagination page, basedSearchObject searchObj,string optionId);
        Task<ScheduleAvailable> GetScheduleAvailableById(string id);
        Task<List<ScheduleAvailable>> GetScheduleAvailableByOptionId(string optionId, string startedDateFormat);

        Task<ScheduleAvailable> AddScheduleAvailable(ScheduleAvailable sa);
        Task<ScheduleAvailable> UpdateScheduleAvailable(ScheduleAvailable sa);
        Task<int> DeleteScheduleAvailable(string id);
    }
}
