using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;

namespace PetKingdomFN.Interfaces
{
    public interface ISellBillRepository
    {
        Task<DataList<SellBill>> GetPageList(Pagination page);
        Task<DataList<SellBill>> SearchSellBill(Pagination page, basedSearchObject searchObj);
        Task<SellBill> GetSellBillById(string id);
        Task<SellBill> AddSellBill(SellBill sb);
        Task<SellBill> UpdateSellBill(SellBill sb);
        Task<int> DeleteSellBill(string id);
    }
}
