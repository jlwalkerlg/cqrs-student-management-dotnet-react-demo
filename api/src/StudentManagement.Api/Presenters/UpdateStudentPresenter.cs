using System.Collections.Generic;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class UpdateStudentPresenter : Presenter
    {
        public UpdateStudentPresenter(Result result) : base(result)
        {
        }

        public override int SuccessStatusCode => 200;

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "Name", "name" },
            { "Email", "email" }
        };
    }
}
