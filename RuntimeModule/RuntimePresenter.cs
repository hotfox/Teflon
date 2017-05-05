using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using Teflon.Infrastructure;
using System.Collections.ObjectModel;

namespace Teflon
{
    [Export(typeof(RuntimePresenter))]
    public class RuntimePresenter:BindableBase
    {
        private ProductPresenter selectedProduct;
        public IRuntime Runtime { get; private set; }
        public ProductPresenter SelectedProduct
        {
            get
            {
                return selectedProduct;
            }
            set
            {
                SetProperty(ref selectedProduct, value);
            }
        }
        public ObservableCollection<ProductPresenter> Products { get; set; } = new ObservableCollection<ProductPresenter>();
        [ImportingConstructor]
        public RuntimePresenter(IRuntime runtime)
        {
            Runtime = runtime;
            foreach(IProduct product in Runtime.Products)
            {
                Products.Add(new ProductPresenter(product));
            }
        }
    }
}
