using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDCS;

namespace Teflon.SDK.Core
{
    public interface ILocalLogger
    {
        void AddVariable(string var_name, string value_string);
        void Log(string log_path);
        void PrintAllVariables(string path);
    }
    public static class Logger
    {
        public static MDCSDeviceSetup MDCSDeviceSetup{ get; set; }
        public static ILocalLogger LocalLogger { get; set; }
        public static string FloatFormatString { get; set; } = "F3";
        public static void OnVariableNameAssginedEvent(object sender,VariableNameAssginedEventArgs e)
        {
            Variable v = sender as Variable;
            if (v == null) return;
            if(MDCSDeviceSetup!=null)
            {
                switch (e.Category)
                {
                    case VariableCategory.Numeric:
                        MDCSDeviceSetup.AddNumericVariable(v.Name, v.ToString());
                        break;
                    case VariableCategory.Float:
                        double value = (DoubleVariable)v;
                        MDCSDeviceSetup.AddNumericVariable(v.Name,value.ToString(FloatFormatString));
                        break;
                    case VariableCategory.Failcode:
                    case VariableCategory.String:
                        {
                            if (v.Name == "Key")
                                MDCSDeviceSetup.Key = v.ToString();
                            else
                                MDCSDeviceSetup.AddStringVariable(v.Name, v.ToString());
                        }
                        break;
                }
            }
            if(LocalLogger!=null)
            {
                if (e.Category == VariableCategory.Float)
                {
                    LocalLogger.AddVariable(v.Name,v.ToString(FloatFormatString));
                }
                else if(e.Category==VariableCategory.Failcode)
                {
                    int value = (FailcodeVariable)v;
                    string msg = FailCodeToMessage.GetErrorMessage(value);
                    LocalLogger.AddVariable(v.Name, msg);
                }
                else
                {
                    LocalLogger.AddVariable(v.Name, v.ToString());
                }
            }
        }
        public static bool Log(string log_path="log.txt")
        {
            bool r1 = true;
            if (LocalLogger != null)
            {
                LocalLogger.Log(log_path);
            }
            if (MDCSDeviceSetup!=null)
            {
                r1 = MDCSDeviceSetup.SendMDCSTestRecord();
                MDCSDeviceSetup.ClearAllVariables();
            }
            return true;
        }
    }
}
