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
            Logger.MDCSDeviceSetup = new MDCSDeviceSetup("AGV_Focus", 
                "http://hsm-mdcsws-ch3u.honeywell.com/MDCSWebService/MDCSService.asmx");
            Logger.LocalLogger = new TXTLogger();

            //... operations to get variable
            StringVaiable sn = "1111111111";
            sn.Name = "SN";
            FailcodeVariable fc = 0;
            fc.Name = "failcode";
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
