using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teflon.SDK.Core
{
    [Flags]
    public enum RuntimeMode
    {
        None=0,
        SkipAssert = 1,
        VirtualNI = 2,
    }
    public static class RuntimeConfiguration
    {
       public static RuntimeMode Mode { get; set; }
        static RuntimeConfiguration()
        {
            Mode = RuntimeMode.None;
        }
    }
}
