using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.Infrastructure;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace Teflon
{
    [Export(typeof(IRuntime))]
    public class Runtime : IRuntime
    {
        [ImportMany]
        IEnumerable<Lazy<IProduct, IProductData>> products;
        private void BuildTestItems(IProduct product)
        {
            foreach(ITestItem test in product.TestItems)
            {
                test.Runtime = this;
            }
        }
        public RuntimeMode Mode { get; set; } = RuntimeMode.Normal;
        public ICollection<IProduct> Products { get; }

        [ImportingConstructor]
        public Runtime()
        {
            Products = new List<IProduct>();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Products";
            if (!Directory.Exists(path))
                return;
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(path));
            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(this);
                foreach (var pair in products)
                {
                    pair.Value.Name = pair.Metadata.Name;
                    BuildTestItems(pair.Value);
                    Products.Add(pair.Value);
                }
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
    }
}
