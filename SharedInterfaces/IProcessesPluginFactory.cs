using System.Collections.Generic;

namespace SharedInterfaces
{
    public interface IProcessesPluginFactory
    {
        IEnumerable<IPlugin> Create();
    }
}