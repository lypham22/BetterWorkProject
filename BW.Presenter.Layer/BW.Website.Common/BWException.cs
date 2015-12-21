using BW.Common.Enums;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace BW.Website.Common
{
    [Serializable]
    public class BWException : Exception
    {
        public ErrorCodeEnum Code { get; set; }

        public BWException()
            : base() { }

        public BWException(string message, ErrorCodeEnum code)
            : base(message) { Code = code; }

        public BWException(string format, ErrorCodeEnum code, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, format, args)) { Code = code; }

        public BWException(string message, ErrorCodeEnum code, Exception innerException)
            : base(message, innerException) { Code = code; }

        public BWException(string format, Exception innerException, params object[] args)
            : base(string.Format(CultureInfo.InvariantCulture, format, args), innerException) { }

        protected BWException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}