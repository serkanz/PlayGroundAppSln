using Domain.Interfaces;

namespace Domain.Abstractions
{
    public abstract class AbstractEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
