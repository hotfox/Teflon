using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Teflon.Infrastructure
{
    public enum RuntimeMode { Normal,SkipSpecification,SkipTestItemFailure}
    public interface IRuntime
    {
        RuntimeMode Mode { get; set; }
        ICollection<IProduct> Products {get;}
    }
}
