using System.Runtime.Serialization;

namespace InnoGotchi.BusinessLogic.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public DataValidationException()
        : base("Incorrect validation data")
        {
        }

        public DataValidationException(string message)
        : base(message)
        {
        }

        protected DataValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
