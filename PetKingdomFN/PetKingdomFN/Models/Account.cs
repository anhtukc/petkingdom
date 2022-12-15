using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            Employees = new HashSet<Employee>();
            ReceiptBills = new HashSet<ReceiptBill>();
            SellBillCustomerAccounts = new HashSet<SellBill>();
            SellBillEmployeeAccounts = new HashSet<SellBill>();
            Shifts = new HashSet<Shift>();
        }

        public string Id { get; set; } = null!;
        public string Permission { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public int? AccessFailedCount { get; set; }
        public bool? LockAccount { get; set; }
        public DateTime? LockTime { get; set; }
        public bool PhoneConfirmed { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ReceiptBill> ReceiptBills { get; set; }
        public virtual ICollection<SellBill> SellBillCustomerAccounts { get; set; }
        public virtual ICollection<SellBill> SellBillEmployeeAccounts { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
