using PetKingdomFN.BusEntities;
using PetKingdomFN.Models;
using SocketIOClient;

namespace PetKingdomFN.Interfaces
{
    public interface ISellBillRepository
    {
        Task<List<CountStatus>> GetAllStatus();
        Task<DataList<SellBill>> GetPageList(Pagination page, basedSearchObject searchObj);
        Task<DataList<SellBill>> GetPageByStatus(Pagination page, basedSearchObject searchObj);
        Task<SellBill> GetSellBillById(string id);
        Task<SellBill> AddSellBill(SellBill sb);
        Task<SellBill> UpdateSellBill(SellBill sb);
        Task<int> DeleteSellBill(string id);

    }
}
