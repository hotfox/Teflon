using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using Teflon.SDK.Core;

namespace Teflon.SDK.Extensions
{
    public static class SerialPortExtensions
    {
        public static string WriteAndReadUntilACK(this SerialPort serial_port,string text,int read_timeout=0,bool throw_exception=true)
        {
            Debug.WriteLine(text);
            serial_port.DiscardInBuffer();
            serial_port.Write(text);
            try
            {
                return ReadUntilACK(serial_port, read_timeout, throw_exception);
            }
            catch (TimeoutException e)
            {
                if (!throw_exception)
                    return string.Empty;
                throw new TeflonCommunicationException(text, 2050, "", e);
            }
        }
        public static string ReadUntilACK(this SerialPort serial_port,int read_timeout=0,bool throw_exception=true)
        {
            string s = string.Empty;
            try
            {
                if (read_timeout > 0)
                    serial_port.ReadTimeout = read_timeout;
                int c = serial_port.ReadChar();
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
            finally
            {
                Debug.WriteLine(s);
            }
        }
    }
}
