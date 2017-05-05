using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Teflon.Infrastructure;


namespace Teflon.Infrastructure
{
    public class TestItem : ITestItem
    {
        public IRuntime Runtime { get; set; }
        public string Name { get; set; }

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
