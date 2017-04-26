using MDCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.SDK;
using Teflon.SDK.Core;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Logger.MDCSDeviceSetup = new MDCSDeviceSetup("Savanna2_BLT", "http://hsm-mdcsws-ch3u.honeywell.com/MDCSWebService/MDCSService.asmx");
            Logger.LocalLogger = new TXTLogger();
            Random r = new Random();

            for (int i = 0; i != 20; ++i)
            {
                IntVariable iv = i;
                iv.Name = "Int";

                DoubleVariable dv = r.NextDouble();
                dv.Name = "Double";

                StringVaiable sv = i.ToString();
                sv.Name = "String";

                BoolVariable bv = i % 2==0 ? true : false;
                bv.Name = "Bool";

                Logger.Log();
            }
        }
    }
}
