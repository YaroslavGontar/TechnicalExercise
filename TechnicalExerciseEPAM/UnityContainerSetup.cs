using EmptyContext;
using Microsoft.Practices.Unity;
using OutputToConsole;
using SharedInterfaces;
using SourceFromParameters;
using TextAnalyzeProcesses;

namespace TechnicalExerciseEPAM
{
    public static class UnityContainerSetup
    {
        public static IUnityContainer RegisterPlugins(this IUnityContainer container)
        {
            container.RegisterType<IContext, Empty>();
            container.RegisterType<IPlugin, SourceFromParametersPlugin>("SourceFromParameters");
            container.RegisterType<IPlugin, TextFilterPlugin>("TextFilter");
            container.RegisterType<IPlugin, AnalyzeNumberOfWordAappearsPlugin>("AnalyzeNumberOfWordAappears");
            container.RegisterType<IPlugin, OutputConsole>("OutputConsole");
            return container;
        }

        public static IUnityContainer RegisterFactories(this IUnityContainer container)
        {
            container.RegisterType<IContextFactory, EmptyContextFactory>();
            container.RegisterType<ISourcePluginFactory, SourceFromParametersFactory>();
            container.RegisterType<IProcessesPluginFactory, TextAnalyzeProcessesFactory>();
            container.RegisterType<IOutputPluginFactory, OutputFctory>();
            return container;
        }

        public static IUnityContainer RegisterInstance<T>(this IUnityContainer container, T instance)
        {
            return container.RegisterInstance(typeof(T), instance);
        }
    }
}