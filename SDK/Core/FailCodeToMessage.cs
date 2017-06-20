using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Teflon.SDK.Core
{
    public delegate string CustomConvert(int failcode);
    public static class FailCodeToMessage
    {
        private static Dictionary<int, string> mapper_;
        public static CustomConvert Convert { get; set; }
        static FailCodeToMessage()
        {
            mapper_ = new Dictionary<int, string>();
            if (File.Exists("failcode.csv"))
            {
                string[] content = File.ReadAllLines("failcode.csv");
                for(int i=1;i!=content.Length;++i)
                {
                    Debug.WriteLine(i);
                    string line = content[i];
                    string[] collection = line.Split(new char[] { ',' });
                    string code_string = collection[0].Trim('\"');
                    string description = collection[2].Trim('\"');
                    int failcode = int.Parse(code_string);
                    mapper_.Add(failcode, description);
                }
            }
        }
        public static string GetErrorMessage(int failcode)
        {
            if (Convert != null)
            {
                return Convert(failcode);
            }
            else
            {
                if (mapper_.ContainsKey(failcode))
                    return mapper_[failcode];
                return failcode.ToString();
            }
        }
    }
}
