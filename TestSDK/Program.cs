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
            SerialPort rs232 = new SerialPort();
            rs232 = new SerialPort("COM2", 115200, Parity.None, 8, StopBits.One);
            rs232.Open();
            rs232.ReadTimeout = 2000;

            string r = rs232.WriteAndReadUntilACK("\x16y\rSERUUI?.");
        }
    }
}
