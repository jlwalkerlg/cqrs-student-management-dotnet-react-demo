using System.Threading;
using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public class RegisterStudentCommand : IAppRequest<Guid>
    {
        public RegisterStudentCommand(string name, string email)
        {
            StudentName = name;
            StudentEmailAddress = email;
        }

        public string StudentName { get; }
        public string StudentEmailAddress { get; }
    }

    public class RegisterStudentCommandHandler : IAppRequestHandler<RegisterStudentCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository repository;
        private readonly INotificationService notificationService;

        public RegisterStudentCommandHandler(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.StudentRepository;
            this.notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(RegisterStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await repository.FindByEmail(command.StudentEmailAddress);
            if (student != null)
                return Result.Fail(new StudentAlreadyRegisteredError());

            student = new Student(
                new StudentName(command.StudentName),
                new EmailAddress(command.StudentEmailAddress)
            );
            await repository.Add(student);
            await unitOfWork.Commit();

            var notification = new StudentRegisteredNotification(student);
            await notificationService.NotifyStudentRegistration(notification);

            return Result.Ok(student.Id);
        }
    }

    public class RegisterStudentCommandValidator : Validator<RegisterStudentCommand>
    {
        public RegisterStudentCommandValidator()
        {
            RuleFor(x => x.StudentName).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.StudentEmailAddress).NotEmpty().WithMessage("Required.");
            RuleFor(x => x.StudentEmailAddress).EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Must be a valid email.");
        }
    }
}
