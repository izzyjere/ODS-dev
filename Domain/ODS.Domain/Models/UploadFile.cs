namespace ODS.Domain.Models
{
    public class UploadFile
    {
        public int OrphanageId { get; set; }
        public string FileName { get; set; }
        public DateTime DateUploaded { get; set; }         
        public virtual Orphanage Orphanage { get; set; }    
    }
}
