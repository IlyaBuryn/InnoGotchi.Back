using System.Runtime.Serialization;

namespace InnoGotchi.BusinessLogic.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public DataValidationException(string message)
        : base(message)
        {
        }

        public DataValidationException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        protected DataValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
