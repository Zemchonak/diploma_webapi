using System;
using System.Runtime.Serialization;

namespace FitnessCenterManagement.BusinessLogic.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message, string fieldName = "")
            : base(message)
        {
            FieldName = fieldName;
        }

        public ValidationException(string message)
            : base(message)
        {
            FieldName = "";
        }

        public ValidationException(string message, Exception innerException, string fieldName = "")
            : base(message, innerException)
        {
            FieldName = fieldName;
        }

        public ValidationException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        public ValidationException()
        {
        }

        protected ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string FieldName { get; set; }
    }
}
