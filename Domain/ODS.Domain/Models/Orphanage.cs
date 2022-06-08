global using ODS.Domain.Contracts;
namespace ODS.Domain.Models
{
    public class Orphanage : Entity<int>
    {   [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int Orphans { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        public bool IsActive { get; set; }
        public List<UploadFile> Files { get; set; }


    }
}
