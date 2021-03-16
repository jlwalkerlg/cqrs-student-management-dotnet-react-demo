namespace StudentManagement.Application.Students
{
    public class StudentNotFoundError : Error
    {
        public StudentNotFoundError() : base("Student not found.")
        {
        }
    }
}
