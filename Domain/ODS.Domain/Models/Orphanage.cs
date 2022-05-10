global using ODS.Domain.Contracts;
namespace ODS.Domain.Models
{
    public class Orphanage : Entity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }


    }
}
