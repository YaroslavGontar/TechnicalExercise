using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedInterfaces;

namespace SourceFromParameters.UnitTest
{
    [TestClass]
    public class SourceFromParametersPluginTest
    {
        [TestMethod]
        public void ConstructorUnityContainerNullResultException()
        {
            try
            {
                var target = new SourceFromParametersPlugin(null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: parameters", exception.Message);
            }
        }

        [TestMethod]
        public void CanProcessContextNullResultFalse()
        {
            var parametrs = new Mock<IParameters>();
            var target = new SourceFromParametersPlugin(parametrs.Object);
            var actual = target.CanProcess(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void ProcessContextNullResultThrowException()
        {
            var parametrs = new Mock<IParameters>();
            var target = new SourceFromParametersPlugin(parametrs.Object);
            try
            {
                target.Process(null);
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Check argument with CanProcess method before run Process.", exception.Message);
            }
        }

        [TestMethod]
        public void ProcessContextParametrsNullResultEmpty()
        {
            var parametrs = new Mock<IParameters>();
            var context = new Mock<IContext>();

            context.SetupSet(c => c.Source = null);
            object actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<object>()).Callback<object>(s => actualResult = s);

            parametrs.SetupGet(p => p.Parameters).Returns((IEnumerable<string>) null);

            var target = new SourceFromParametersPlugin(parametrs.Object);
            target.Process(context.Object);

            Assert.AreEqual(string.Empty, actualResult);
            parametrs.VerifyAll();
            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextParametrsStringResultString()
        {
            var parametrs = new Mock<IParameters>();
            var context = new Mock<IContext>();

            context.SetupSet(c => c.Source = null);
            object actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<object>()).Callback<object>(s => actualResult = s);

            parametrs.SetupGet(p => p.Parameters).Returns(new List<string> {"Test string", "Test"});

            var target = new SourceFromParametersPlugin(parametrs.Object);
            target.Process(context.Object);

            Assert.AreEqual("Test string Test", actualResult);
            parametrs.VerifyAll();
            context.VerifyAll();
        }
    }

    [TestClass]
    public class SourceFromParametersFactoryTest
    {
        [TestMethod]
        public void ConstructorUnityContainerNullResultException()
        {
            try
            {
                var target = new SourceFromParametersFactory(null);
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
                .Returns((IPlugin)null);

            var target = new SourceFromParametersFactory(container.Object);

            var actual = target.CreateSource();

            Assert.IsNull(actual);

            Assert.AreEqual(1, actualPluginNames.Count);
            Assert.IsTrue(actualPluginNames.Contains("SourceFromParameters"));

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

            var target = new SourceFromParametersFactory(container.Object);

            var actual = target.CreateSource();

            Assert.IsNotNull(actual);
            Assert.AreEqual(plugin.Object, actual);

            Assert.AreEqual(1, actualPluginNames.Count);
            Assert.IsTrue(actualPluginNames.Contains("SourceFromParameters"));

            container.VerifyAll();
            plugin.VerifyAll();
        }

    }
}