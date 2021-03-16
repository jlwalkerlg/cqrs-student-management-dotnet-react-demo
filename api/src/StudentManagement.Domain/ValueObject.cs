namespace StudentManagement.Domain
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        protected abstract bool IsEqual(T other);
        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            var other = obj as ValueObject<T>;
            if (other is null) return false;

            return IsEqual((T)other);
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
