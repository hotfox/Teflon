using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teflon.SDK.Core;

namespace Teflon.SDK.Utilities
{
    public class MCXParser
    {
        public String MCX { get; private set; }
        public MCXParser(string mcx)
        {
            MCX = mcx;
        }
        public virtual bool IsValid() { return false; }
        public virtual string GetBoardType() { throw new FormatException(MCX); }
        public virtual Product Create() { throw new NotImplementedException(MCX); }
    }
}
