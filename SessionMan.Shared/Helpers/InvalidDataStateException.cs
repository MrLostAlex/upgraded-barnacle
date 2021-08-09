using System;
using System.Globalization;

namespace SessionMan.Shared.Helpers
{
    public class InvalidDataStateException : Exception
    {
        public InvalidDataStateException(string message) : base(message) { }

        public InvalidDataStateException(string message, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, message, args))
        {
        }
    }
}