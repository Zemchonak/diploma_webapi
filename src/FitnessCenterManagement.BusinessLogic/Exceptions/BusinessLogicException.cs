using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FitnessCenterManagement.BusinessLogic.Exceptions
{
    [Serializable]
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }

        public BusinessLogicException(string message, string fieldName = "", IReadOnlyDictionary<string, int> info = null)
            : base(message)
        {
            FieldName = fieldName;
            Info = info;
        }

        public BusinessLogicException(string message, Exception innerException, string fieldName = "", IReadOnlyDictionary<string, int> info = null)
        : base(message, innerException)
        {
            FieldName = fieldName;
            Info = info;
        }

        public BusinessLogicException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        public BusinessLogicException()
        {
        }

        protected BusinessLogicException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string FieldName { get; set; }

        public IReadOnlyDictionary<string, int> Info { get; set; }
    }
}
