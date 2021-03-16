namespace StudentManagement.Application.Students
{
    public class StudentAlreadyRegisteredError : Error
    {
        public StudentAlreadyRegisteredError() : base("Student already registered.")
        {
        }
    }
}
