using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDCS;

namespace Teflon.SDK.Core
{
    public interface ITrackEqualAssert<T>
    {
        void TrackEuqalAssert(Variable v,T target);
    }
    public interface ITrackInRangeAssert<T>
    {
        void TrackInRangeAssert(Variable v,T min,T max);
    }

    public enum MDCSVariableCategory { String,Numeric,Failcode}
    public class VariableNameAssginedEventArgs:EventArgs
    {
        public MDCSVariableCategory Category { get; set; }
    }
    public delegate void VaribaleNameAssginedEventHandler(object sender, VariableNameAssginedEventArgs e);
    public abstract class Variable
    {
        private string name_;
        protected virtual void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
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
                if (!string.IsNullOrWhiteSpace(name_))
                {
                    throw new DuplicateVariableException(string.Format("Name property has value:{0}", name_));
                }
                name_ = value;
                OnVariableNameAssginedEvent(new VariableNameAssginedEventArgs());
            }
        }
        public virtual string Unit { get; set; } = string.Empty;
        public virtual string ToString(string format)
        {
            return ToString();
        }
    }
    public class DoubleVariable: Variable
    {
        protected double value_;
        protected DoubleVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.Category = MDCSVariableCategory.Numeric;
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
        public override string ToString(string format)
        {
            return value_.ToString(format);
        }
        public virtual void AssertInRange(double min,double max,int less_than_error_code,int larger_than_error_code, ITrackInRangeAssert<double> tracker=null)
        {
            if (tracker != null)
                tracker.TrackInRangeAssert(this, min, max);
            if (RuntimeConfiguration.Mode.HasFlag(RuntimeMode.SkipAssert))
                return;
            if (value_ < min)
                throw new TeflonSpecificationException(this,less_than_error_code);
            if (value_ > max)
                throw new TeflonSpecificationException(this,larger_than_error_code);
        }
        public virtual void AssertInRange(double min,double max,int error_code,ITrackInRangeAssert<double> tracker=null)
        {
            AssertInRange(min, max, error_code, error_code,tracker);
        }
    }
    public class IntVariable : Variable
    {
        protected int value_;
        protected IntVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.Category = MDCSVariableCategory.Numeric;
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
        public virtual void AssertEqual(int target,int error_code,ITrackEqualAssert<int> tracker)
        {
            if (tracker != null)
                tracker.TrackEuqalAssert(this, target);
            if (RuntimeConfiguration.Mode.HasFlag(RuntimeMode.SkipAssert))
                return;
            if (value_ != target)
                throw new TeflonSpecificationException(this,error_code);
        }
    }
    public class BoolVariable : Variable
    {
        protected bool value_;
        protected BoolVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.Category = MDCSVariableCategory.String;
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
        public virtual void AssertEqual(bool target, int error_code, ITrackEqualAssert<bool> tracker)
        {
            if (tracker != null)
                tracker.TrackEuqalAssert(this, target);
            if (RuntimeConfiguration.Mode.HasFlag(RuntimeMode.SkipAssert))
                return;
            if (value_ != target)
                throw new TeflonSpecificationException(this,error_code);
        }
    }

    public class VoltageVariable:DoubleVariable
    {
        public static implicit operator Double(VoltageVariable v)
        {
            return v.value_;
        }
        public static implicit operator VoltageVariable(Double value)
        {
            VoltageVariable v = new VoltageVariable();
            v.value_ = value;
            v.Unit = "V";
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }
    }
    public class CurrentVariable : DoubleVariable
    {
        public static implicit operator Double(CurrentVariable v)
        {
            return v.value_;
        }
        public static implicit operator CurrentVariable(Double value)
        {
            CurrentVariable v = new CurrentVariable();
            v.value_ = value;
            v.Unit = "A";
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
            e.Category = MDCSVariableCategory.String;
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

    public class FailcodeVariable:Variable
    {
        protected int value_;
        protected FailcodeVariable() : base() { }
        protected override void OnVariableNameAssginedEvent(VariableNameAssginedEventArgs e)
        {
            e.Category = MDCSVariableCategory.Failcode;
            base.OnVariableNameAssginedEvent(e);
        }
        public static implicit operator FailcodeVariable(int failcode)
        {
            FailcodeVariable v = new FailcodeVariable();
            v.value_ = failcode;
            v.VariableNameAssginedEvent += Logger.OnVariableNameAssginedEvent;
            return v;
        }

        public static implicit operator Int32(FailcodeVariable v)
        {
            return v.value_;
        }
        public override string ToString()
        {
            return value_.ToString();
        }
    }
}
