namespace SharpPusher.Services
{
    public class Response<T>
    {
        private readonly ErrorCollection _errors = new ErrorCollection();

        public ErrorCollection Errors
        {
            get { return _errors; }
        }

        public T Result { get; set; }
    }
}