using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SharedInterfaces;

namespace TextAnalyzeProcesses
{
    public class TextAnalyzeProcessesFactory : IProcessesPluginFactory
    {
        private readonly IUnityContainer _container;

        public TextAnalyzeProcessesFactory(IUnityContainer container)
        {
            if(container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        public IEnumerable<IPlugin> Create()
        {
            return new List<IPlugin>()
            {
                _container.Resolve<IPlugin>("TextFilter"),
                _container.Resolve<IPlugin>("AnalyzeNumberOfWordAappears")
            }.Where(p => p != null);
        }
    }
}
