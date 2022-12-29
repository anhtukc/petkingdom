using PetKingdomFN.Models;

namespace PetKingdomFN.BusEntities
{
    public class postingObject
    {
        public Pagination page { get; set; } = null!; 
        public basedSearchObject searchObj { get; set; }
    }
    public class postingObjectWithId
    {
        public Pagination page { get; set; } = null!;
        public basedSearchObject? searchObj { get; set; }
        public string Id { get; set; }
    }

    public class CustomerAccount
    {
        public Account acc { get; set; } = null!;
        public Customer cus { get; set; } = null!;
        public string file { get; set; }
    }

    }
