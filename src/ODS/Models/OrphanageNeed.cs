﻿using ODS.Enums;

namespace ODS.Models
{
    public class OrphanageNeed : Entity<int>
    {
        [Required]
        public string Description { get; set; }
        public int OrphanageId { get; set; }
        public virtual Orphanage Orphanage { get; set; }
        [Required, Range(1, 4)]
        public DonationType Type { get; set; }
        [Required, Range(1, double.MaxValue)]
        public double Target { get; set; }
        public double Raised { get; set; }
        public DateTime? MonthStart { get; set; }
        public OrphanageNeed()
        {
            MonthStart = DateTime.Now;
        }
        public string GetTarget()
        {
            return Type == DonationType.Money ? Target + " ZMW" : Convert.ToInt32(Target) + " items";
        }
        public string GetRaised()
        {
            return Type == DonationType.Money ? Raised + " ZMW" : Convert.ToInt32(Raised) + " items";
        }

    }
}
