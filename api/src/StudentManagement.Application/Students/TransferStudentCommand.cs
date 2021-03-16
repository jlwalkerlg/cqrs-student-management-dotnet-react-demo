using System.Threading;
using System;
using System.Threading.Tasks;
using FluentValidation;
using StudentManagement.Application.Courses;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public class TransferStudentCommand : IAppRequest
    {
        public TransferStudentCommand(Guid studentId, Guid fromCourseId, Guid toCourseId, string grade)
        {
            StudentId = studentId;
            FromCourseId = fromCourseId;
            ToCourseId = toCourseId;
            Grade = grade;
        }

        public Guid StudentId { get; }
        public Guid FromCourseId { get; }
        public Guid ToCourseId { get; }
        public string Grade { get; }
    }

    public class TransferStudentCommandHandler : AppRequestHandler<TransferStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public TransferStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            studentRepository = unitOfWork.StudentRepository;
            courseRepository = unitOfWork.CourseRepository;
        }

        public override async Task<Result> Handle(TransferStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await studentRepository.FindById(command.StudentId);
            if (student == null)
                return Result.Fail(new StudentNotFoundError());

            var fromCourse = await courseRepository.FindById(command.FromCourseId);
            if (fromCourse == null)
                return Result.Fail(new CourseNotFoundError());

            var toCourse = await courseRepository.FindById(command.ToCourseId);
            if (toCourse == null)
                return Result.Fail(new CourseNotFoundError());

            if (!student.AlreadyEnrolledIn(fromCourse))
                return Result.Fail(new Error($"Student not enrolled in {fromCourse.Title.Title}"));

            student.Transfer(fromCourse, toCourse, Enum.Parse<Grade>(command.Grade));
            await unitOfWork.Commit();

            return Result.Ok();
        }
    }

    public class TransferStudentCommandValidator : Validator<TransferStudentCommand>
    {
        public TransferStudentCommandValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.FromCourseId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.ToCourseId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.Grade).IsEnumName(typeof(Grade))
                .WithMessage($"Must be one of: {String.Join(',', Enum.GetNames(typeof(Grade)))}.");
        }
    }
}
