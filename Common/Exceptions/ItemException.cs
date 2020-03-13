using System;

namespace Common.Exceptions
{
    public class ItemException : Exception
    {
        public ItemException(string message, string prop) : base(message)
        {
            Property = prop;
        }

        public string Property { get; protected set; }
    }
}
