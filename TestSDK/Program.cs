using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.SDK.Core;
using Teflon.SDK.Utilities;
using MDCS;
using Teflon.SDK.Extensions;
using System.IO.Ports;

namespace TestSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.LocalLogger = new TXTLogger();
            IntVariable a = 1;
            a.Name = "a";
            DoubleVariable b = 2.2;
            b.Unit = "db";
            b.Name = "b";
            CurrentVariable c = 3.3;
            c.Name = "c";
            VoltageVariable d = 4.4;
            d.Name = "d";
            BoolVariable e = true;
            e.Name = "e";
            StringVaiable f = "ffff";
            f.Name = "f";
            FailcodeVariable g = 0;
            g.Name = "g";
            Logger.Log();
        }
    }
}
