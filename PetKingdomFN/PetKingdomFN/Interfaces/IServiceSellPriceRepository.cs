using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface IServiceSellPriceRepository
    {
        Task<DataList<ServiceSellPrice>> GetPageList(Pagination page);
        Task<DataList<ServiceSellPrice>> SearchServiceSellPrice(Pagination page, basedSearchObject searchObj);
        Task<ServiceSellPrice> GetServiceSellPriceById(string id);
        Task<ServiceSellPrice> AddServiceSellPrice(ServiceSellPrice ssp);
        Task<ServiceSellPrice> UpdateServiceSellPrice(ServiceSellPrice ssp);
        Task<int> DeleteServiceSellPrice(string id);
    }
}
