using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Application.Courses
{
    public class GetCoursesQuery : IAppRequest<List<CourseDto>>
    {
    }

    public class GetCoursesQueryHandler : IAppRequestHandler<GetCoursesQuery, List<CourseDto>>
    {
        private readonly ICourseDtoRepository repository;

        public GetCoursesQueryHandler(ICourseDtoRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<List<CourseDto>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return Result.Ok(await repository.Get());
        }
    }
}
