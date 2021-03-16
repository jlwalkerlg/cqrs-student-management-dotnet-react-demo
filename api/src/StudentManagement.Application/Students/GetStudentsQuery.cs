using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;

namespace StudentManagement.Application.Students
{
    public class GetStudentsQuery : IAppRequest<List<StudentDto>>
    {
        public GetStudentsQuery(int? numberOfCourses = null, Guid? enrolledIn = null)
        {
            NumberOfCourses = numberOfCourses;
            EnrolledIn = enrolledIn;
        }

        public int? NumberOfCourses { get; }
        public Guid? EnrolledIn { get; }
    }

    public class GetStudentsQueryHandler : IAppRequestHandler<GetStudentsQuery, List<StudentDto>>
    {
        private readonly IStudentDtoRepository repository;

        public GetStudentsQueryHandler(IStudentDtoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<List<StudentDto>>> Handle(GetStudentsQuery query, CancellationToken cancellationToken)
        {
            var filter = new GetStudentsFilter
            {
                NumberOfCourses = query.NumberOfCourses,
                EnrolledIn = query.EnrolledIn,
            };
            var students = await repository.GetStudents(filter);
            return Result.Ok<List<StudentDto>>(students);
        }
    }

    public class GetStudentsQueryValidator : Validator<GetStudentsQuery>
    {
        public GetStudentsQueryValidator()
        {
            RuleFor(x => x.NumberOfCourses)
                .InclusiveBetween(0, 2)
                .When(x => x.NumberOfCourses.HasValue)
                .WithMessage("Must be between 0 and 2.");

            RuleFor(x => x.EnrolledIn)
                .NotEqual(Guid.Empty)
                .When(x => x.EnrolledIn.HasValue)
                .WithMessage("Required.");
        }
    }
}
