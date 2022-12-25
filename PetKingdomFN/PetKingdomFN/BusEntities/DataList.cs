using PetKingdomFN.Models;

namespace PetKingdomFN.BusEntities
{

    public class DataList<T>
    {
        public List<T> list { get; set; } 
        public int numberOfRecords { get; set; }
    }
}
