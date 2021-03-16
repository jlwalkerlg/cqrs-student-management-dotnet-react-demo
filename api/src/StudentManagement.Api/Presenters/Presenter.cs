using System.Collections.Generic;
using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public abstract class Presenter
    {
        public Presenter(Result result)
        {
            IsSuccess = result.IsSuccess;

            if (result.IsFailure)
            {
                Error = result.Error;

                if (Error.IsType<ValidationError>())
                {
                    Errors = new Dictionary<string, List<string>>();

                    var error = (ValidationError)result.Error;
                    foreach (var item in error.Errors)
                    {
                        if (FieldMap.ContainsKey(item.Key))
                            Errors.Add(FieldMap[item.Key], item.Value);
                        else
                            Errors.Add(item.Key, item.Value);
                    }
                }
            }
        }

        protected virtual Dictionary<string, string> FieldMap { get; } = new Dictionary<string, string>();

        public bool IsSuccess { get; }
        public abstract int SuccessStatusCode { get; }
        public virtual object Content { get; }
        public Dictionary<string, List<string>> Errors { get; }
        public Error Error { get; }
    }
}
