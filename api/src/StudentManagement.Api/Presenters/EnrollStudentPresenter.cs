using System.Collections.Generic;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class EnrollStudentPresenter : Presenter
    {
        public EnrollStudentPresenter(Result result) : base(result)
        {
        }

        public override int SuccessStatusCode => 201;

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "StudentId", "studentId" },
            { "CourseId", "courseId" },
            { "Grade", "grade" },
        };
    }
}
