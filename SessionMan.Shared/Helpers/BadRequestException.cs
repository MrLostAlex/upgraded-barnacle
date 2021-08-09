using System;
using System.Globalization;

namespace SessionMan.Shared.Helpers
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string title, string message) : base(message) { }

        public BadRequestException(string title, string message, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, message, args))
        {
        }
    }
}