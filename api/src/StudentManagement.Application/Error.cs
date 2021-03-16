namespace StudentManagement.Application
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public bool IsType<T>()
        {
            return GetType() == typeof(T);
        }
    }
}
