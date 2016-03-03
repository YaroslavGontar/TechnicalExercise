using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SharedInterfaces;

namespace OutputToConsole
{
    public class OutputFctory : IOutputPluginFactory
    {
        private IUnityContainer _container;

        public OutputFctory(IUnityContainer container)
        {
            _container = container;
        }

        public IPlugin CreateOutput()
        {
            return _container.Resolve<IPlugin>("OutputConsole");
        }
    }
}
