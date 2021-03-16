using System.Threading.Tasks;
using StudentManagement.Application.Students;

namespace StudentManagement.Application
{
    public interface INotificationService
    {
        Task NotifyStudentRegistration(StudentRegisteredNotification notification);
    }
}
