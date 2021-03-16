using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Application;
using StudentManagement.Application.Students;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class NotificationServiceSpy : INotificationService
    {
        public List<StudentRegisteredNotification> Registrations { get; } = new List<StudentRegisteredNotification>();

        public Task NotifyStudentRegistration(StudentRegisteredNotification notification)
        {
            Registrations.Add(notification);
            return Task.CompletedTask;
        }
    }
}
