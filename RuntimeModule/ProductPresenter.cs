using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Teflon.Infrastructure;

namespace Teflon
{
    public class ProductPresenter:BindableBase
    {
        private ObservableCollection<TestItemPresenter> testItems;
        public string Name
        {
            get
            {
                return Product.Name;
            }
        }
        public UIElement View
        {
            get
            {
                return Product.View;
            }
        }
        public ObservableCollection<TestItemPresenter> TestItems
        {
            get
            {
                return testItems;
            }
        }

        public IProduct Product { get; private set; }
        public ProductPresenter(IProduct product)
        {
            Product = product;
            testItems = new ObservableCollection<TestItemPresenter>();
            foreach(ITestItem testItem in product.TestItems)
            {
                testItems.Add(new TestItemPresenter(testItem));
            }
        }
    }
}
