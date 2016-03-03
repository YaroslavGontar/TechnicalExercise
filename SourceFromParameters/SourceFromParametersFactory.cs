using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SharedInterfaces;

namespace SourceFromParameters
{
    public class SourceFromParametersFactory : ISourcePluginFactory
    {
        private readonly IUnityContainer _unityContainer;

        public SourceFromParametersFactory(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public IPlugin CreateSource()
        {
            return _unityContainer.Resolve<IPlugin>("SourceFromParameters");
        }
    }
}
