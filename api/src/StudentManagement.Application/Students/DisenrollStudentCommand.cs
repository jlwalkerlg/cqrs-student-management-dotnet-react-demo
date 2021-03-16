using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using StudentManagement.Application.Courses;

namespace StudentManagement.Application.Students
{
    public class DisenrollStudentCommand : IAppRequest
    {
        public DisenrollStudentCommand(Guid studentId, Guid courseId, string comment)
        {
            StudentId = studentId;
            CourseId = courseId;
            Comment = comment;
        }

        public Guid StudentId { get; }
        public Guid CourseId { get; }
        public string Comment { get; }
    }

    public class DisenrollStudentCommandHandler : AppRequestHandler<DisenrollStudentCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public DisenrollStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            studentRepository = unitOfWork.StudentRepository;
            courseRepository = unitOfWork.CourseRepository;
        }

        public override async Task<Result> Handle(DisenrollStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await studentRepository.FindById(command.StudentId);
            if (student == null)
                return Result.Fail(new StudentNotFoundError());

            var course = await courseRepository.FindById(command.CourseId);
            if (course == null)
                return Result.Fail(new CourseNotFoundError());

            if (!student.AlreadyEnrolledIn(course))
                return Result.Fail(new Error($"Student not enrolled in {course.Title.Title}"));

            student.Disenroll(course, command.Comment);
            await unitOfWork.Commit();

            return Result.Ok();
        }
    }

    public class DisenrollStudentCommandValidator : Validator<DisenrollStudentCommand>
    {
        public DisenrollStudentCommandValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.CourseId).NotEmpty().WithMessage("Required.");

            RuleFor(x => x.Comment).NotEmpty().WithMessage("Required.");
        }
    }
}
