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
    }
    public static class Logger
    {
        public static MDCSDeviceSetup MDCSDeviceSetup{ get; set; }
        public static ILocalLogger LocalLogger { get; set; }
        public static string FloatFormatString { get; set; } = "F3";
        public static void OnVariableNameAssginedEvent(object sender,VariableNameAssginedEventArgs e)
        {
            if(MDCSDeviceSetup!=null)
            {
                switch (e.Category)
                {
                    case VariableCategory.Numeric:
                        MDCSDeviceSetup.AddNumericVariable(e.VariableName, e.VariableValue);
                        break;
                    case VariableCategory.Float:
                        double value;
                        Double.TryParse(e.VariableValue, out value);
                        MDCSDeviceSetup.AddNumericVariable(e.VariableName,value.ToString(FloatFormatString));
                        break;
                    case VariableCategory.FailCode:
                    case VariableCategory.String:
                        {
                            if (e.VariableName == "Key")
                                MDCSDeviceSetup.Key = e.VariableValue;
                            else
                                MDCSDeviceSetup.AddStringVariable(e.VariableName, e.VariableValue);
                        }
                        break;
                }
            }
            if(LocalLogger!=null)
            {
                if (e.Category == VariableCategory.Float)
                {
                    double value;
                    Double.TryParse(e.VariableValue, out value);
                    LocalLogger.AddVariable(e.VariableName, value.ToString(FloatFormatString));
                }
                else if(e.Category==VariableCategory.FailCode)
                {
                    int value;
                    int.TryParse(e.VariableValue, out value);
                    string msg = FailCodeToMessage.GetErrorMessage(value);
                    LocalLogger.AddVariable(e.VariableName, msg);
                }
                else
                {
                    LocalLogger.AddVariable(e.VariableName, e.VariableValue);
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
