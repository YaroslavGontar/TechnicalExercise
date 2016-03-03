using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedInterfaces;

namespace TextAnalyzeProcesses.UnitTest
{
    [TestClass]
    public class TextAnalyzeProcessesFactoryTest
    {
        [TestMethod]
        public void ConstructorUnityContainerNullResultException()
        {
            try
            {
                var target = new TextAnalyzeProcessesFactory(null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: container", exception.Message);
            }
        }

        [TestMethod]
        public void CreateUnityContainerResolveNullResultEmpty()
        {
            var container = new Mock<IUnityContainer>();
            var actualPluginNames = new List<string>();
            container.Setup(c => c.Resolve(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<ResolverOverride[]>()))
                .Callback<Type, string, ResolverOverride[]>((t, s, p) => actualPluginNames.Add(s))
                .Returns((IPlugin) null);

            var target = new TextAnalyzeProcessesFactory(container.Object);

            var actual = target.Create();

            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count());

            Assert.AreEqual(2, actualPluginNames.Count);
            Assert.IsTrue(actualPluginNames.Contains("TextFilter"));
            Assert.IsTrue(actualPluginNames.Contains("AnalyzeNumberOfWordAappears"));

            container.VerifyAll();
        }

        [TestMethod]
        public void CreateUnityContainerResolvePluginResult2Plugin()
        {
            var container = new Mock<IUnityContainer>();
            var plugin = new Mock<IPlugin>();
            var actualPluginNames = new List<string>();
            container.Setup(c => c.Resolve(It.IsAny<Type>(), It.IsAny<string>(), It.IsAny<ResolverOverride[]>()))
                .Callback<Type, string, ResolverOverride[]>((t, s, p) => actualPluginNames.Add(s))
                .Returns(plugin.Object);

            var target = new TextAnalyzeProcessesFactory(container.Object);

            var actual = target.Create();

            Assert.IsNotNull(actual);
            var actualList = actual.ToList();
            Assert.AreEqual(2, actualList.Count);
            Assert.AreEqual(plugin.Object, actualList[0]);
            Assert.AreEqual(plugin.Object, actualList[1]);

            Assert.AreEqual(2, actualPluginNames.Count);
            Assert.IsTrue(actualPluginNames.Contains("TextFilter"));
            Assert.IsTrue(actualPluginNames.Contains("AnalyzeNumberOfWordAappears"));

            container.VerifyAll();
            plugin.VerifyAll();
        }
    }
}