using System.Runtime.Serialization;

namespace InnoGotchi.BusinessLogic.Exceptions
{
    public class AuthenticateException : Exception
    {
        public AuthenticateException()
            : base("Username or password is incorrect!")
        {
        }
    }
}
