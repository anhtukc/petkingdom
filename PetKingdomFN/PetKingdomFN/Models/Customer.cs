﻿using System;
using System.Collections.Generic;

namespace PetKingdomFN.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Pets = new HashSet<Pet>();
        }

        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Sex { get; set; }
        public string? Image { get; set; }
        public int? Status { get; set; }
        public string? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
