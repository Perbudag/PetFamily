namespace PetFamily.Domain.Shared.Models
{
    public abstract class Entity<T> : IEquatable<Entity<T>> 
        where T : notnull
    {
        public T Id { get; }

        protected Entity(T id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if(obj is Entity<T> entity && entity.GetHashCode() == GetHashCode()) return true;

            return false;
        }

        public bool Equals(Entity<T>? other) =>
            Equals((object?)other);
        
        public override string ToString() =>
            Id.ToString()!;

        public static bool operator ==(Entity<T> left, Entity<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right)
        {
            return !left.Equals(right);
        }
    }
}
