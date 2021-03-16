using System.Collections.Generic;
using System;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class RegisterStudentPresenter : Presenter
    {
        public RegisterStudentPresenter(Result<Guid> result) : base(result)
        {
            if (result.IsSuccess)
            {
                Content = new RegisterStudentResponse
                {
                    Id = result.Value,
                };
            }
        }

        public override int SuccessStatusCode => 201;
        public override object Content { get; }

        protected override Dictionary<string, string> FieldMap => new Dictionary<string, string>
        {
            { "StudentName", "name" },
            { "StudentEmailAddress", "email" },
        };
    }

    public class RegisterStudentResponse
    {
        public Guid Id { get; set; }
    }
}
