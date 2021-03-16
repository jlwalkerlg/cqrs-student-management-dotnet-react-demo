using System.Collections.Generic;
using StudentManagement.Application;
using StudentManagement.Application.Courses;

namespace StudentManagement.Api.Presenters
{
    public class GetCoursesPresenter : Presenter
    {
        public GetCoursesPresenter(Result<List<CourseDto>> result) : base(result)
        {
            if (result.IsSuccess)
            {
                Content = result.Value;
            }
        }

        public override int SuccessStatusCode => 200;
        public override object Content { get; }
    }
}
