using System;

namespace StudentManagement.Domain.Courses
{
    public class Credits : ValueObject<Credits>
    {
        public int Amount { get; }

        public Credits(int amount)
        {
            if (amount < 1)
                throw new InvalidOperationException();

            Amount = amount;
        }

        protected override bool IsEqual(Credits other)
        {
            return Amount == other.Amount;
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode();
        }
    }
}
