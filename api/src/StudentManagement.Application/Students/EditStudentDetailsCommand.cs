using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public class EditStudentDetailsCommand : IAppRequest
    {
        public EditStudentDetailsCommand(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }

    public class EditStudentDetailsCommandHandler : AppRequestHandler<EditStudentDetailsCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository repository;

        public EditStudentDetailsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.StudentRepository;
        }

        public override async Task<Result> Handle(EditStudentDetailsCommand command, CancellationToken cancellationToken)
        {
            var student = await repository.FindById(command.Id);
            if (student == null)
                return Result.Fail(new StudentNotFoundError());

            student.Name = new StudentName(command.Name);
            student.Email = new EmailAddress(command.Email);
            await unitOfWork.Commit();

            return Result.Ok();
        }
    }

    public class EditStudentDetailsCommandValidator : Validator<EditStudentDetailsCommand>
    {
        public EditStudentDetailsCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Required.");
            RuleFor(x => x.Email).EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Must be a valid email.");
        }
    }
}
