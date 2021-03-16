using System.Threading;
using System;
using System.Threading.Tasks;
using FluentValidation;

namespace StudentManagement.Application.Students
{
    public class UnregisterStudentCommand : IAppRequest
    {
        public UnregisterStudentCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; }
    }

    public class UnregisterStudentCommandHandler : AppRequestHandler<UnregisterStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository repository;

        public UnregisterStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repository = unitOfWork.StudentRepository;
        }

        public override async Task<Result> Handle(UnregisterStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await repository.FindById(command.StudentId);
            if (student == null)
                return Result.Fail(new StudentNotFoundError());

            await repository.Remove(student);
            await unitOfWork.Commit();

            return Result.Ok();
        }
    }

    public class UnregisterStudentCommandValidator : Validator<UnregisterStudentCommand>
    {
        public UnregisterStudentCommandValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("Required.");
        }
    }
}
