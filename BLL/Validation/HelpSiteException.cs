using System.Runtime.Serialization;

namespace BLL.Validation
{
    [Serializable()]
    public class HelpSiteException : Exception
    {
        public HelpSiteException() : base() { }

        public HelpSiteException(string message) : base(message) { }

        public HelpSiteException(string message, Exception inner) : base(message, inner) { }

        protected HelpSiteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
