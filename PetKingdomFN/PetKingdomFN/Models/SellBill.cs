using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class SellBill
    {
        public SellBill()
        {
            Schedules = new HashSet<Schedule>();
            SellBillDetails = new HashSet<SellBillDetail>();
        }

        public string Id { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public long TotalPaid { get; set; }
        public string? CustomerAccountId { get; set; }
        public string? EmployeeAccountId { get; set; }
        public int? Status { get; set; }

        public virtual Account? CustomerAccount { get; set; }
        public virtual Account? EmployeeAccount { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<SellBillDetail> SellBillDetails { get; set; }
    }
}
