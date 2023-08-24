using System;

namespace RfId.Core.Exceptions
{
    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(Type type) : base($"Unknown Class Type: {type}")
        {
            
        }

        public UnknownTypeException(string type) : base($"Unknown Class Type: {type}")
        {

        }
    }
}