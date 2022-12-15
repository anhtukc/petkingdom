using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetKingdomFN.BusEntities
{
    public class Pagination
    {
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public string sortColumn { get; set; }
        public string sortOrder { get; set; }
    }
}
