using Domain.Interfaces;

namespace Domain.Abstractions
{
    public abstract class AbstractEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
