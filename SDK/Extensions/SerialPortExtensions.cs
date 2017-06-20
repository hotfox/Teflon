using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Teflon.SDK.Extensions
{
    public static class SerialPortExtensions
    {
        public static string SendAndWaitResponse(this SerialPort serial_port,string text,int read_timeout=0)
        {
            serial_port.DiscardInBuffer();
            if (read_timeout > 0)
                serial_port.ReadTimeout = read_timeout;
            serial_port.Write(text);
            int c = serial_port.ReadChar();
            string s = string.Empty;
            while (c != 6)
            {
                s += (char)c;
                c = serial_port.ReadChar();
            }
            return s;
        }
    }
}
