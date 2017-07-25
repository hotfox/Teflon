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
    class Program: ITrackInRangeAssert<double>
    {
        static int Main(string[] args)
        {
            Logger.MDCSDeviceSetup = new MDCSDeviceSetup("TestPost", 
                "http://hsm-mdcsws-ch3u.honeywell.com/MDCSWebService/MDCSService.asmx");
            Logger.LocalLogger = new TXTLogger();

            //... operations to get variable
            IntVariable a = 1;
            a.Name = "a";

            DoubleVariable b = 2.2;
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
            g.Name = "failcode";

            Logger.Log();
            return 0;
        }

        void ITrackInRangeAssert<double>.TrackInRangeAssert(Variable v, double min, double max)
        {
            throw new NotImplementedException();
        }
    }
    public class Tracker : ITrackInRangeAssert<double>
    {
        void ITrackInRangeAssert<double>.TrackInRangeAssert(Variable v, double min, double max)
        {
            throw new NotImplementedException();
        }
    }
}
