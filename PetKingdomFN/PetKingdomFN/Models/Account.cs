using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetKingdomFN.Models;

public partial class Account
{
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

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    [NotMapped]
    public Employee? employee { get; set; }

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<ReceiptBill> ReceiptBills { get; } = new List<ReceiptBill>();

    public virtual ICollection<SellBill> SellBillCustomerAccounts { get; } = new List<SellBill>();

    public virtual ICollection<SellBill> SellBillEmployeeAccounts { get; } = new List<SellBill>();

    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();
}
