using System;

namespace StudentManagement.Domain.Students
{
    public class StudentName : ValueObject<StudentName>
    {
        public string Name { get; }

        public StudentName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new InvalidOperationException();

            Name = name;
        }

        protected override bool IsEqual(StudentName other)
        {
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
