using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Teflon.SDK.Core;

namespace Teflon.SDK
{
    public class TXTLogger:ILocalLogger
    {
        private  Dictionary<string, string> buffer = new Dictionary<string, string>();
        private  string oneLine;
        private  string header;
        private  List<string> variableNames = new List<string>();
        private void AddHeader(string log_path)
        {
            int length = variableNames.Count;
            for (int i = 0; i != length; ++i)
            {
                string name = variableNames[i];
                if (i == length - 1)
                {
                    header += name.PadRight(name.Length + FixedWidth);
                }
                else
                {
                    header += (name + ",").PadRight(name.Length + FixedWidth);
                }
            }
            using (StreamWriter sw = new StreamWriter(log_path, true))
            {
                sw.WriteLine(header);
            }
        }
        private void UpdateHeader(string log_path)
        {
            if (!File.Exists(log_path))
            {
                File.CreateText(log_path).Close();
                AddHeader(log_path);
            }
            else
            {
                string[] content = File.ReadAllLines(log_path);
                if (content.Length == 0)
                {
                    AddHeader(log_path);
                }
                else
                {
                    string[] names = content[0].Split(new char[] { ','});
                    if(names.Length!=variableNames.Count)
                    {
                        File.Delete(log_path);
                        AddHeader(log_path);
                    }
                    else
                    {
                        int i;
                        for(i=0;i!=names.Length;++i)
                        {
                            if (!names[i].Trim().Equals(variableNames[i]))
                                break;
                        }
                        if(i!=names.Length)
                        {
                            File.Delete(log_path);
                            AddHeader(log_path);
                        }
                    }
                }
            }
        }
        public int FixedWidth { get; set; } = 12;
        public  void AddVariable(string var_name,string value_string)
        {
            variableNames.Add(var_name);
            buffer[var_name] = value_string;
        }
        public void Log(string log_path)
        {
            UpdateHeader(log_path);
            int length = variableNames.Count;
            for (int i = 0; i != length; ++i)
            {
                string name = variableNames[i];
                string value = buffer[name];
                if (i == length - 1)
                {
                    oneLine += value.PadRight(name.Length + FixedWidth);
                }
                else
                {
                    oneLine += (value + ",").PadRight(name.Length + FixedWidth);
                }
            }
            using (StreamWriter sw = new StreamWriter(log_path, true))
            {
                sw.WriteLine(oneLine);
            }
            oneLine = string.Empty;
            variableNames.Clear();
            buffer.Clear();
        }
    }
}
