using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teflon.Infrastructure;

namespace Teflon.Infrastructure
{

    public class Product:IProduct
    {
        [ImportMany]
        IEnumerable<Lazy<ITestItem, ITestItemData>> testItems;

        [ImportingConstructor]
        protected Product()
        {
            TestItems = new List<ITestItem>();
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(this.GetType().Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            try
            {
                container.ComposeParts(this);
                foreach(var pair in testItems)
                {
                    pair.Value.Name = pair.Metadata.Name;
                    TestItems.Add(pair.Value);
                }
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public string Name { get; set; }
        public ICollection<ITestItem> TestItems { get; }
        public virtual UIElement View { get; }
    }
}
