using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Jungle.Exceptions
{
    public class JungleException : ApplicationException
    {
        public JungleException()
        {
        }

        public JungleException(string message) : base(message)
        {
        }

        public JungleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected JungleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
