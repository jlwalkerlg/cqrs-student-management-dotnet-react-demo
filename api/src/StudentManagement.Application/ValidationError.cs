using System.Collections.Generic;

namespace StudentManagement.Application
{
    public class ValidationError : Error
    {
        public ValidationError() : base("Invalid input.")
        {
        }

        public Dictionary<string, List<string>> Errors { get; } = new Dictionary<string, List<string>>();

        public void AddError(string field, string message)
        {
            if (!Errors.ContainsKey(field))
                Errors.Add(field, new List<string>());
            Errors[field].Add(message);
        }

        public bool Any()
        {
            foreach (var item in Errors)
            {
                if (item.Value.Count != 0)
                    return true;
            }
            return false;
        }

        public bool Has(string field)
        {
            return Errors.ContainsKey(field);
        }
    }
}
