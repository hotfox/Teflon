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
    public class TeflonDoubleAssertFailException: TeflonSpecificationException
    { 
        public TeflonDoubleAssertFailException(Variable v,double lowLimit, double highLimit,int error_code,string message="",Exception innerException = null):
            base(v,error_code,message,innerException)
        {
            LowLimit = lowLimit;
            HighLimit = highLimit;
        }
        public double LowLimit { get; private set; }
        public double HighLimit { get; private set; }
    }
    public class TeflonIntAssertFailException:TeflonSpecificationException
    {
        public TeflonIntAssertFailException(Variable v, int target, int error_code, string message = "", Exception innerException = null):
            base(v,error_code,message,innerException)
        {
            Target = target;
        }
        public int Target { get; private set; }
    }
    public class TeflonStringAssertFailException:TeflonSpecificationException
    {
        public TeflonStringAssertFailException(Variable v, string target, int error_code, string message = "", Exception innerException = null):
            base(v,error_code,message,innerException)
        {
            Target = target;
        }
        public string Target { get; private set; }
    }
    public class TeflonCommunicationException:TeflonTestException
    {
        public TeflonCommunicationException(string command,int error_code, string message = "", Exception innerException = null) : base(error_code,message,innerException)
        {
            Command = command;
        }
        public string Command { get; private set; }
    }
    public class TeflonBoolAssertFailException:TeflonSpecificationException
    {
        public TeflonBoolAssertFailException(Variable v, bool target, int error_code, string message = "", Exception innerException = null):
            base(v,error_code,message,innerException)
        {
            Target = target;
        }
        public bool Target { get; private set; }
    }
}
