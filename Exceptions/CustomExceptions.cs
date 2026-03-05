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
#pragma warning disable SYSLIB0051 // Type or member is obsolete
        protected LoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#pragma warning restore SYSLIB0051 // Type or member is obsolete
    }
    public class CasinoException : Exception
    {
        public CasinoException(){}
        public CasinoException(string message) : base(message) {}
        public CasinoException(string message, Exception inner) : base(message, inner) {}
        
    }
}