using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MojKatalog.Helpers.Exceptions
{
    [Serializable]
    public class ExistingUsernameException : Exception
    {
        public ExistingUsernameException()
        : base() { }

        public ExistingUsernameException(string message)
        : base(message) { }

        public ExistingUsernameException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public ExistingUsernameException(string message, Exception innerException)
        : base(message, innerException) { }

        public ExistingUsernameException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

        protected ExistingUsernameException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
    }


    [Serializable]
    public class ExistingEmailException : Exception
    {
        public ExistingEmailException()
        : base()
        { }

        public ExistingEmailException(string message)
        : base(message)
        { }

        public ExistingEmailException(string format, params object[] args)
        : base(string.Format(format, args))
        { }

        public ExistingEmailException(string message, Exception innerException)
        : base(message, innerException)
        { }

        public ExistingEmailException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException)
        { }

        protected ExistingEmailException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        { }
    }
}