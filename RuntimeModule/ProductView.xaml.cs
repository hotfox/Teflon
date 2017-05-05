using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Teflon
{
    /// <summary>
    /// ProductView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(ProductView))]
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }
        [ImportingConstructor]
        public ProductView(RuntimePresenter presenter):this()
        {
            DataContext = presenter;
            if (presenter.Products.Count > 0)
                return;
        }
    }
}
