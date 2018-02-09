using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Teflon.SDK.Scanner
{
    public interface IGenerateSN
    {
        string GenerateSN();
    }

    public class F0705Generator:IGenerateSN
    {
        public string F0705URI { get; set; } = @"\\ch3uw1050\Test\Masters\F0705\mfgserial.exe";
        public string OutfileURI { get; set; }= @"\\ch3uw1050\Test\Masters\F0705\serout.txt";

        public string GenerateSN()
        {
            if (string.IsNullOrEmpty(F0705URI))
                throw new ArgumentNullException();
            if (!File.Exists(F0705URI))
                throw new FileNotFoundException(F0705URI);
            using (Process process = new Process())
            {
                process.StartInfo.FileName = F0705URI;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();
                if(!File.Exists(OutfileURI))
                    throw new FileNotFoundException(OutfileURI);
                return File.ReadAllText(OutfileURI);
            }
        }
    }
}
