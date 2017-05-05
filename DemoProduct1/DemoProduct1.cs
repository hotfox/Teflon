using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.Infrastructure;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DemoProduct1
{
    [Export(typeof(IProduct))]
    [ExportMetadata("Name", "DemoProduct1")]
    public class DemoProduct1 : Product
    {
        private Image image = new Image();
        public override UIElement View { get; } = new View();
    }
    [Export(typeof(ITestItem))]
    [ExportMetadata("Name", "DemoProduct1.PowerOn")]
    public class TestItem1:TestItem
    {
        public override void Execute()
        {
            Console.WriteLine(Runtime.Mode.ToString());
        }
    }
    [Export(typeof(ITestItem))]
    [ExportMetadata("Name", "DemoProduct1.PowerOff")]
    public class TestItem2 : TestItem
    {
        public override void Execute()
        {
            Console.WriteLine(Runtime.Mode.ToString());
        }
    }
}
