using System.Threading.Tasks;
using StudentManagement.Application;
using StudentManagement.Application.Students;

namespace StudentManagement.Api.Services
{
    public class NotificationService : INotificationService
    {
        public Task NotifyStudentRegistration(StudentRegisteredNotification notification)
        {
            return Task.CompletedTask;
        }
    }
}
