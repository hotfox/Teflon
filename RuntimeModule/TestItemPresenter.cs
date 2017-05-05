using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teflon.Infrastructure;

namespace Teflon
{
    public class TestItemPresenter:BindableBase
    {
        public ITestItem TestItem { get; private set; }
        public string Name
        {
            get
            {
                return TestItem.Name;
            }
        }
        public bool Checked { get; set; }
        public TestItemPresenter(ITestItem testItem)
        {
            TestItem = testItem;
        }
    }
}
