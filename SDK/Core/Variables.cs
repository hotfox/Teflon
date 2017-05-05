using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDCS;

namespace Teflon.SDK.Core
{
    public enum VariableCategory { String,Numeric,Float,FailCode}
    public class VariableNameAssginedEventArgs:EventArgs
    {
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public string VariableUnit { get; set; }
        public VariableCategory Category { get; set; }

    }
    public delegate void VaribaleNameAssginedEventHandler(object sender, VariableNameAssginedEventArgs e);
    public abstract class Variable
    {
        private string name_;
        protected  virtual void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            VariableNameAssginedEvent(this, e);
        }
        public event VaribaleNameAssginedEventHandler VariableNameAssginedEvent;
        public string Name
        {
            get
            {
                return name_;
            }
            set
            {
                if(!string.IsNullOrWhiteSpace(name_))
                {
                    throw new ArgumentException(string.Format("Name property has value:{0}",name_), value);
                }
                name_ = value;
                OnVariableNameAssginedEvent(new VariableNameAssginedEventArgs());
            }
        }
    }
    public class DoubleVariable:Variable
    {
        protected double value_;
        protected DoubleVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableName = Name;
            e.VariableValue = this.ToString();
            e.Category = VariableCategory.Float;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator Double(DoubleVariable v)
        {
            return v.value_;
        }
        public static implicit operator DoubleVariable(Double value)
        {
            DoubleVariable v = new DoubleVariable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
        public override string ToString()
        {
            return value_.ToString();
        }
    }
    public class IntVariable : Variable
    {
        protected int value_;
        protected IntVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableName = Name;
            e.VariableValue = this.ToString();
            e.Category = VariableCategory.Numeric;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator Int32(IntVariable v)
        {
            return v.value_;
        }
        public static implicit operator IntVariable(int value)
        {
            IntVariable v = new IntVariable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
        public override string ToString()
        {
            return value_.ToString();
        }
    }
    public class BoolVariable : Variable
    {
        protected bool value_;
        protected BoolVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableName = Name;
            e.VariableValue = this.ToString();
            e.Category = VariableCategory.String;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator Boolean(BoolVariable v)
        {
            return v.value_;
        }
        public static implicit operator BoolVariable(bool value)
        {
            BoolVariable v = new BoolVariable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
        public override string ToString()
        {
            return value_ ? "High" : "Low";
        }
    }

    public class VoltageVariable:DoubleVariable
    {
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableUnit = "V";
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator Double(VoltageVariable v)
        {
            return v.value_;
        }
        public static implicit operator VoltageVariable(Double value)
        {
            VoltageVariable v = new VoltageVariable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
    }
    public class CurrentVariable : DoubleVariable
    {
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableUnit = "A";
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator Double(CurrentVariable v)
        {
            return v.value_;
        }
        public static implicit operator CurrentVariable(Double value)
        {
            CurrentVariable v = new CurrentVariable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
    }

    public class StringVaiable:Variable
    {
        protected string value_;
        protected StringVaiable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableName = Name;
            e.VariableValue = this.ToString();
            e.Category = VariableCategory.String;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator String(StringVaiable v)
        {
            return v.value_;
        }
        public static implicit operator StringVaiable(String value)
        {
            StringVaiable v = new StringVaiable();
            v.value_ = value;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
        public override string ToString()
        {
            return value_;
        }
    }

    public class FailCodeVariable:Variable
    {
        protected int value_;
        protected FailCodeVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.VariableName = Name;
            e.VariableValue = this.ToString();
            e.Category = VariableCategory.FailCode;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator FailCodeVariable(int failcode)
        {
            FailCodeVariable v = new FailCodeVariable();
            v.value_ = failcode;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }

        public override string ToString()
        {
            return value_.ToString();
        }
    }
}
