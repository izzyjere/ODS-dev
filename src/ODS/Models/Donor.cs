﻿namespace ODS.Models
{
    public class Donor : Entity<int>
    {
        public string Name { get => FirstName + " " + LastName; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual List<Donation> Donations { get; set; }
        public virtual List<Payment> Payments { get; set; }

    }
}
