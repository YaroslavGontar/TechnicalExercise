using System;
using System.Collections.Generic;
using System.Linq;
using EmptyContext;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OutputToConsole;
using SharedInterfaces;
using SourceFromParameters;
using TextAnalyzeProcesses;

namespace TechnicalExerciseEPAM.UnitTest
{
    [TestClass]
    public class UnityContainerSetupTest
    {
        class RegistryTestItem
        {
            public Type From { get; set; }
            public Type To { get; set; }
            public object ObjTo { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void RegisterPluginsResultDone()
        {
            var container = new Mock<IUnityContainer>();

            var actualRegistrated = new List<RegistryTestItem>();

            container.Setup(
                c =>
                    c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(),
                        It.IsAny<InjectionMember[]>()))
                .Callback<Type, Type, string, LifetimeManager, InjectionMember[]>(
                    (tFrom, tTo, name, life, injMems) =>
                        actualRegistrated.Add(new RegistryTestItem() {From = tFrom, To = tTo, Name = name}));

            container.Object.RegisterPlugins();

            Assert.IsTrue(actualRegistrated.Any(r=>r.From == typeof(IContext) && r.To == typeof(EmptyContext.Empty)));
            Assert.IsTrue(actualRegistrated.Any(r =>
                r.From == typeof (IPlugin) && r.To == typeof (SourceFromParametersPlugin) &&
                r.Name == "SourceFromParameters"));

            Assert.IsTrue(actualRegistrated.Any(r =>
                r.From == typeof (IPlugin) && r.To == typeof (TextFilterPlugin) &&
                r.Name == "TextFilter"));

            Assert.IsTrue(actualRegistrated.Any(r =>
                r.From == typeof (IPlugin) && r.To == typeof (AnalyzeNumberOfWordAappearsPlugin) &&
                r.Name == "AnalyzeNumberOfWordAappears"));

            Assert.IsTrue(actualRegistrated.Any(r =>
                r.From == typeof (IPlugin) && r.To == typeof (OutputConsole) &&
                r.Name == "OutputConsole"));

            container.VerifyAll();
        }

        [TestMethod]
        public void RegisterFactoriesResultDone()
        {
            var container = new Mock<IUnityContainer>();

            var actualRegistrated = new List<RegistryTestItem>();

            container.Setup(
                c =>
                    c.RegisterType(It.IsAny<Type>(), It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<LifetimeManager>(),
                        It.IsAny<InjectionMember[]>()))
                .Callback<Type, Type, string, LifetimeManager, InjectionMember[]>(
                    (tFrom, tTo, name, life, injMems) =>
                        actualRegistrated.Add(new RegistryTestItem() { From = tFrom, To = tTo, Name = name }));

            container.Object.RegisterFactories();

            Assert.IsTrue(
                actualRegistrated.Any(r => r.From == typeof (IContextFactory) && r.To == typeof (EmptyContextFactory)));
            Assert.IsTrue(
                actualRegistrated.Any(
                    r => r.From == typeof (ISourcePluginFactory) && r.To == typeof (SourceFromParametersFactory)));
            Assert.IsTrue(
                actualRegistrated.Any(
                    r => r.From == typeof (IProcessesPluginFactory) && r.To == typeof (TextAnalyzeProcessesFactory)));
            Assert.IsTrue(
                actualRegistrated.Any(r => r.From == typeof (IOutputPluginFactory) && r.To == typeof (OutputFctory)));

            container.VerifyAll();
        }

        [TestMethod]
        public void RegisterInstanceResultDone()
        {
            var container = new Mock<IUnityContainer>();

            var actualRegistrated = new List<RegistryTestItem>();

            container.Setup(
                c =>
                    c.RegisterInstance(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<LifetimeManager>()))
                .Callback<Type, string, object, LifetimeManager>(
                    (tFrom, name, obj, life) =>
                        actualRegistrated.Add(new RegistryTestItem() { From = tFrom, ObjTo = obj, Name = name }));

            var plugin = new Mock<IPlugin>();

            container.Object.RegisterInstance<IPlugin>(plugin.Object);

            Assert.IsTrue(actualRegistrated.Any(r => r.From == typeof (IPlugin) && r.ObjTo == plugin.Object));

            container.VerifyAll();
        }

    }
}