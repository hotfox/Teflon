using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Teflon.SDK.Scanner
{
    public delegate int TestDelegate();
    public class Product:IDisposable
    {
        public Context Context { get; private set; }
        //不要直接使用这个属性添加测项，设为公共是为了兼容现有的测试代码
        public List<KeyValuePair<string, TestDelegate>> Tests = new List<KeyValuePair<string, TestDelegate>>();
        public int AppendTest(string name,TestDelegate test)
        {
            Tests.Add(new KeyValuePair<string, TestDelegate>(name, test));
            return 0;
        }
        public int RemoveTest(string name)
        {
            var l = from item in Tests
                    where item.Key == name
                    select item;
            if(l.Count()!=0)
            {
                Tests.Remove(l.First());
            }
            return 0;
        }
        public virtual int Run(Context context=null)
        {
            Context = context;
            foreach(var pair in Tests)
            {
                if(context!=null)
                    Context.DisplayMessage("Start " + pair.Key+"\r\n");
                int r = pair.Value.Invoke();
                if (r != 0)
                    return r;
                if(context!=null)
                    Context.DisplayMessage(pair.Key + " Finished\r\n");
            }
            return 0;
        }
        public virtual int DebugRun(IEnumerable<string> names,Context context=null)
        {
            Context = context;
            foreach (string name in names)
            {
                var value = from pair in Tests
                            where pair.Key == name
                            select pair.Value;
                if (value.Count() == 0)
                    continue;
                if (context != null)
                    Context.DisplayMessage("Start " + name + "\r\n");
                int r = value.First().Invoke();
                if (r != 0)
                    return r;
                if (context != null)
                    Context.DisplayMessage(name + " Fnished\r\n");
            }
            return 0;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            return;
        }
    }
}
