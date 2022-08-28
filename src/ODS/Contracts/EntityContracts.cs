global using System.ComponentModel.DataAnnotations;


namespace ODS.Contracts
{
    public interface IEntity
    {

    }
    public interface IEntity<TKey> : IEntity
    {

        TKey Id { get; set; }
    }
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
    public interface IAuditableEntity<TKey> : IEntity<TKey>
    {
        TKey Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string? LastModifiedBy { get; set; }
        DateTime? LastModifiedOn { get; set; }

    }
    public abstract class AuditableEntity<TKey> : IAuditableEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
