using System.Collections.Generic;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class TransferStudentPresenter : Presenter
    {
        public TransferStudentPresenter(Result result) : base(result)
        {
        }

        public override int SuccessStatusCode => 200;

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "StudentId", "studentId" },
            { "FromCourseId", "fromCourseId" },
            { "ToCourseId", "toCourseId" },
            { "Grade", "grade" },
        };
    }
}
