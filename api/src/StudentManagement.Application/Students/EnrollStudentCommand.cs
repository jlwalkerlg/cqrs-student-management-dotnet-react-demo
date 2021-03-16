using System.Threading;
using System;
using System.Threading.Tasks;
using FluentValidation;
using StudentManagement.Application.Courses;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public class EnrollStudentCommand : IAppRequest
    {
        public EnrollStudentCommand(Guid studentId, Guid courseId, string grade)
        {
            StudentId = studentId;
            CourseId = courseId;
            Grade = grade;
        }

        public Guid StudentId { get; }
        public Guid CourseId { get; }
        public string Grade { get; }
    }

    public class EnrollStudentCommandHandler : AppRequestHandler<EnrollStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public EnrollStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            studentRepository = unitOfWork.StudentRepository;
            courseRepository = unitOfWork.CourseRepository;
        }

        public override async Task<Result> Handle(EnrollStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await studentRepository.FindById(command.StudentId);
            if (student == null)
                return Result.Fail(new StudentNotFoundError());

            var course = await courseRepository.FindById(command.CourseId);
            if (course == null)
                return Result.Fail(new CourseNotFoundError());

            if (student.AlreadyEnrolledIn(course))
                return Result.Fail(new Error($"Student already enrolled in {course.Title.Title}"));

            student.Enroll(course, Enum.Parse<Grade>(command.Grade));
            await unitOfWork.Commit();

            return Result.Ok();
        }
    }

    public class EnrollStudentCommandValidator : Validator<EnrollStudentCommand>
    {
        public EnrollStudentCommandValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.CourseId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.Grade).IsEnumName(typeof(Grade))
                .WithMessage($"Must be one of: {String.Join(',', Enum.GetNames(typeof(Grade)))}.");
        }
    }
}
