namespace StudentManagement.Application
{
    public interface IValidator<TRequest>
    {
        Result Validate(TRequest request);
    }
}
