using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Teflon.Infrastructure
{
    public interface ITestItem
    {
        void Execute();
        string Name { get; set; }
        IRuntime Runtime { get; set; }
    }
    public interface ITestItemData
    {
        string Name { get; }
    }
}
