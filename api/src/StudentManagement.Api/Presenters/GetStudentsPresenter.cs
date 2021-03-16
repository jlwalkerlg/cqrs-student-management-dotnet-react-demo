using System.Collections.Generic;
using StudentManagement.Application;
using StudentManagement.Application.Students;

namespace StudentManagement.Api.Presenters
{
    public class GetStudentsPresenter : Presenter
    {
        public GetStudentsPresenter(Result<List<StudentDto>> result) : base(result)
        {
            if (result.IsSuccess)
            {
                Content = result.Value;
            }
        }

        public override int SuccessStatusCode => 200;
        public override object Content { get; }

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "NumberOfCourses", "numberOfCourses" },
            { "EnrolledIn", "enrolledIn" }
        };
    }
}
