using System;
using System.Runtime.Serialization;
namespace ConsoleApp129.Exceptions
{

    public class SaveException : Exception
    {   
    public SaveException() { }
    public SaveException(string message) : base(message) { }
    public SaveException(string message, Exception inner) : base(message, inner) { }
    }
    public class LoadException : Exception
    {
        public LoadException(string message) : base(message) { }
        public LoadException(string message, Exception inner) : base(message, inner) { }
        protected LoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}