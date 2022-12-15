using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Provider
    {
        public Provider()
        {
            ReceiptBills = new HashSet<ReceiptBill>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? Status { get; set; }

        public virtual ICollection<ReceiptBill> ReceiptBills { get; set; }
    }
}
