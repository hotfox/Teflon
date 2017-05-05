using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition.Hosting;
using Prism.Mef.Modularity;
using Teflon.Infrastructure;
using Prism.Regions;

namespace Teflon
{
    [ModuleExport(typeof(RuntimeModule))]
    public class RuntimeModule : IModule
    {
        public IRegionManager RegionManager { get; private set; }
        [ImportingConstructor]
        public RuntimeModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
        }

        public void Initialize()
        {
            RegionManager.RegisterViewWithRegion("ProductView",typeof(ProductView));
            RegionManager.RegisterViewWithRegion("ConfigurationView", typeof(ConfigurationView));
        }
    }
}
