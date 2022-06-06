using System;

namespace WebApiSecurityDemo.Utils
{
    public class ExampleException : Exception
    {
        public ExampleException(string message) : base(message)
        {
        }

        public ExampleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}