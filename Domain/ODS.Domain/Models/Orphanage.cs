global using ODS.Domain.Contracts;
namespace ODS.Domain.Models
{
    public class Orphanage : Entity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Orphans { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        public List<UploadFile> Files { get; set; }


    }
}
