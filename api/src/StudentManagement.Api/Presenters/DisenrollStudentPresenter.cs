using System.Collections.Generic;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class DisenrollStudentPresenter : Presenter
    {
        public DisenrollStudentPresenter(Result result) : base(result)
        {
        }

        public override int SuccessStatusCode => 204;

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "StudentId", "studentId" },
            { "CourseId", "courseId" },
            { "Comment", "comment" },
        };
    }
}
