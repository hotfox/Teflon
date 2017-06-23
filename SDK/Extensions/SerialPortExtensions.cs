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
        public static string WriteAndReadUntilACK(this SerialPort serial_port,string text,int read_timeout=0,bool throw_exception=true)
        {
            serial_port.DiscardInBuffer();
            serial_port.Write(text);
            return ReadUntilACK(serial_port, read_timeout,throw_exception);
        }
        public static string ReadUntilACK(this SerialPort serial_port,int read_timeout=0,bool throw_exception=true)
        {
            try
            {
                if (read_timeout > 0)
                    serial_port.ReadTimeout = read_timeout;
                int c = serial_port.ReadChar();
                string s = string.Empty;
                while (c != 6)
                {
                    s += (char)c;
                    c = serial_port.ReadChar();
                }
                return s;
            }
            catch(TimeoutException e)
            {
                if (!throw_exception)
                    return string.Empty;
                throw e;
            }
        }
    }
}
