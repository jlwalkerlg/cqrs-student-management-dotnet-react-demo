using System;

namespace StudentManagement.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; } = Guid.NewGuid();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as Entity;
            if (other is null) return false;

            if (GetType() != other.GetType()) return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
