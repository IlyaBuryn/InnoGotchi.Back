using System.Runtime.Serialization;

namespace InnoGotchi.BusinessLogic.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException()
            :base()
        { }

        public NotFoundException(string message)
            : base($"This entity doesn't exist: {message}!")
        {
        }
    }
}
