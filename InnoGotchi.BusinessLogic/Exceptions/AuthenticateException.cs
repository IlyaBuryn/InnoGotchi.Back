using System.Runtime.Serialization;

namespace InnoGotchi.BusinessLogic.Exceptions
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException()
            : base("Unauthorized user!")
        {
        }

        public AuthenticateException(string? message)
            : base(message)
        {
        }
    }
}
