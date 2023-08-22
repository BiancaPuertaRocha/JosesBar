

namespace JosesBarAPI.Exceptions
{
    [Serializable]
    public class InternalServerError : Exception
    {
        public InternalServerError() : base() { }
        public InternalServerError(string message) : base(message) { }
        public InternalServerError(string message, Exception inner) : base(message, inner) { }
    }
}