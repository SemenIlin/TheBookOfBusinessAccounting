using System;

namespace Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, string prop) : base(message)
        {
            Property = prop;
        }

        public string Property { get; protected set; }
    }
}
