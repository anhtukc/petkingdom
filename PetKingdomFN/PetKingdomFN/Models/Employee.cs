using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models;

public partial class Employee
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string IdentityCardNumber { get; set; } = null!;

    public string? Sex { get; set; }

    public int? Status { get; set; }

    public string? AccountId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Account? Account { get; set; }
}
