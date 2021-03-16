using System;
using System.Text.RegularExpressions;

namespace StudentManagement.Domain.Students
{
    public class EmailAddress : ValueObject<EmailAddress>
    {
        private static Regex emailRegex = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");

        public string Address { get; }

        public EmailAddress(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
                throw new InvalidOperationException();

            if (!emailRegex.IsMatch(address))
                throw new InvalidOperationException();

            Address = address;
        }

        protected override bool IsEqual(EmailAddress obj)
        {
            var other = obj as EmailAddress;
            if (other == null) return false;

            return Address == other.Address;
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }
    }
}
