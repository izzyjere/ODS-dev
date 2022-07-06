﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODS.Domain.Models
{
    public class OrphanageNeed : Entity<int>
    {
        public string Description { get; set; }
        public int OpharnageId { get; set; }
        public virtual Orphanage Orphanage { get; set; }
        public DonationType Type { get; set; }
        public double Target { get; set; }
        public double Raised { get; set; }
        public DateTime CreatedOn { get; set;}
        public OrphanageNeed()
        {
            CreatedOn = DateTime.Now;
        }
        public string GetTarget()
        {
            return (Type == DonationType.Money) ? Target + " ZMW" : Convert.ToInt32(Target) + " items";
        }
        public string GetRaised()
        {
            return (Type == DonationType.Money) ? Raised + " ZMW" : Convert.ToInt32(Raised) + " items";
        }

    }
}
