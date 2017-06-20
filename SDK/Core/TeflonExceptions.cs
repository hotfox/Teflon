using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teflon.SDK.Core
{
    public class TeflonException:Exception
    {
        protected TeflonException(string message, Exception innerException=null) : base(message, innerException) { }
    }

    public class DuplicateVariableException:TeflonException
    {
        public DuplicateVariableException(string message, Exception innerException = null) : base(message, innerException) { }
    }

    public class TeflonTestException:TeflonException
    {
        public int ErrorCode { get; private set; }
        public TeflonTestException(int error_code, string message="", Exception innerException = null) : base(message, innerException)
        {
            ErrorCode = error_code;
        }
    }
    public class TeflonSpecificationException : TeflonTestException
    {
        public TeflonSpecificationException(Variable v, int error_code, string message = "", Exception innerException = null) : base(error_code, message, innerException)
        {
            Variable = v;
        }
        public Variable Variable { get; private set; }
    }
}
