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
        private Dictionary<string, string> buffer;
        private  string oneLine;
        private  string header;
        private List<string> variableNames;
        private string[] pure_names_;
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
        private bool UpdateHeader(string log_path)
        {
            if (!File.Exists(log_path))
            {
                File.CreateText(log_path).Close();
                AddHeader(log_path);
                return false;
            }
            else
            {
                string[] content = File.ReadAllLines(log_path);
                if (content.Length == 0)
                {
                    AddHeader(log_path);
                    return false;
                }
                else
                {
                    string[] names = content[0].Split(new char[] { ',' });
                    var pure_names = from name in names
                                     select name.Trim();
                    pure_names_ = pure_names.ToArray();
                    var not_in_pure_names = from v_name in variableNames
                                            where pure_names.Contains(v_name)==false
                                            select v_name;
                    if(not_in_pure_names.Count()!=0)
                    {
                        File.Delete(log_path);
                        AddHeader(log_path);
                        return false;
                    }
                    else
                    {
                        var not_in_vnames = from name in pure_names
                                            where variableNames.Contains(name) == false
                                            select name;
                        if(not_in_vnames.Count()!=0)
                        {
                            foreach(var lack_name in not_in_vnames)
                            {
                                AddVariable(lack_name, string.Empty);
                            }
                            return true;
                        }
                    }
                    return false;
                }
            }
        }
        private void InsertDateTime()
        {
            const string name = "DateTime";
            if(!variableNames.Contains(name))
                variableNames.Insert(0, name);
            if (!buffer.Keys.Contains(name))
                buffer.Add(name, DateTime.Now.ToString());
            else
                buffer[name] = DateTime.Now.ToString();
        }
        public TXTLogger()
        {
            buffer = new Dictionary<string, string>();
            variableNames = new List<string>();
            InsertDateTime();
        }
        public int FixedWidth { get; set; } = 17;
        public  void AddVariable(string var_name,string value_string)
        {
            variableNames.Add(var_name);
            buffer[var_name] = value_string;
        }
        public void Log(string log_path)
        {
            InsertDateTime();
            bool updated = UpdateHeader(log_path);
            int length = variableNames.Count;
            for (int i = 0; i != length; ++i)
            {
                string name = updated? pure_names_[i]:variableNames[i];
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
            InsertDateTime();
        }
    }
}
