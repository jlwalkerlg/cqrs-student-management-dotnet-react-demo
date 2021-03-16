using StudentManagement.Application;

namespace StudentManagement.Api.Presenters
{
    public class UnregisterStudentPresenter : Presenter
    {
        public UnregisterStudentPresenter(Result result) : base(result)
        {
        }

        public override int SuccessStatusCode => 204;
    }
}
