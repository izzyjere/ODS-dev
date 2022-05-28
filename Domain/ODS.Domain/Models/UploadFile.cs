using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODS.Domain.Models
{
    public class UploadFile  : Entity<int>    
    {
        public int OrphanageId { get; set; }
        public string FileName { get; set; }
        public DateTime DateUploaded { get; set; }
        public string FilePath { get; set; }
        public virtual Orphanage Orphanage { get; set; }    
    }
}
