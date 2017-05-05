using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace Teflon.Infrastructure
{
    public interface IProduct
    {
        string Name { get; set; }
        UIElement View { get; }
        ICollection<ITestItem> TestItems { get; }
    }
    public interface IProductData
    {
        string Name { get; }
    }
}
