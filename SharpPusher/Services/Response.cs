namespace SharpPusher.Services
{
    public class Response<T> : ErrorCollection
    {
        public T Result { get; set; }
    }
}