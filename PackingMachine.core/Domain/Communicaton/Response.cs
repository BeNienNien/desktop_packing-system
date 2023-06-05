using PackingMachine.Core.Domain.Communication;

namespace PackingMachine.core.Domain.Communication
{
    public class Response<T>
    {
        public bool Success { get; private set; }
        public Error Error { get; private set; }
        public T Resource { get; private set; }

        public Response(T resource)
        {
            Success = true;
            Error = null;
            Resource = resource;
        }

        public Response(Error error)
        {
            Success = false;
            Error = error;
            Resource = default;
        }
    }
}