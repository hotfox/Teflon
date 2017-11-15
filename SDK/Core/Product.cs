using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teflon.SDK.Core
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
        public virtual int Run(Context context)
        {
            Context = context;
            foreach(var pair in Tests)
            {
                Context.DisplayMessage("Start " + pair.Key+"\r\n");
                int r = pair.Value.Invoke();
                if (r != 0)
                    return r;
                Context.DisplayMessage(pair.Key + " Fnished\r\n");
            }
            return 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
