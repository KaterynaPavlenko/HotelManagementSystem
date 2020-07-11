using System;

namespace HotelManagementSystem.BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, string property) : base(message)
        {
            Property = property;
        }

        public string Property { get; protected set; }
    }
}