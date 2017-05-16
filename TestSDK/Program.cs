using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.SDK.Core;
using Teflon.SDK;
using MDCS;

namespace TestSDK
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mdcsDevice = new MDCS.MDCSDeviceSetup("Savanna2_BLT", "http://hsm-mdcsws-ch3u.honeywell.com/MDCSWebService/MDCSService.asmx");
            //Logger.MDCSDeviceSetup = mdcsDevice;
            Logger.LocalLogger = new TXTLogger();
            for (int i = 0; i != 2; ++i)
            {
              //  FailCodeVariable v = 5418;
               // v.Name = "Failcode";

                BoolVariable b = false;
                b.Name = "False";
                Logger.Log();
            }
        }
    }
}
