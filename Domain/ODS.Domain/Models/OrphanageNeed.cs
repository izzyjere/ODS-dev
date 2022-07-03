using System;
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
    }
}
