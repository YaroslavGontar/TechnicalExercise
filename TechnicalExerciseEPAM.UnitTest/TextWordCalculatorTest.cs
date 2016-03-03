using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedInterfaces;

namespace TechnicalExerciseEPAM.UnitTest
{
    [TestClass]
    public class TextWordCalculatorTest
    {
        [TestMethod]
        public void ConstructorUnityContainerNullResultException()
        {
            try
            {
                var target = new TextWordCalculator(null, null, null, null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: contextFactory", exception.Message);
            }

            try
            {
                var contextFactory = new Mock<IContextFactory>();
                var target = new TextWordCalculator(contextFactory.Object, null, null, null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: sourceFactory", exception.Message);
            }

            try
            {
                var contextFactory = new Mock<IContextFactory>();
                var sourceFactory = new Mock<ISourcePluginFactory>();
                var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, null, null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: processesFactory", exception.Message);
            }

            try
            {
                var contextFactory = new Mock<IContextFactory>();
                var sourceFactory = new Mock<ISourcePluginFactory>();
                var processesFactory = new Mock<IProcessesPluginFactory>();
                var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, processesFactory.Object, null);
            }
            catch (ArgumentNullException exception)
            {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: outputFactory", exception.Message);
            }

        }

        [TestMethod]
        public void ProcessResultDone()
        {
            var contextFactory = new Mock<IContextFactory>();
            var sourceFactory = new Mock<ISourcePluginFactory>();
            var processesFactory = new Mock<IProcessesPluginFactory>();
            var outputFactory = new Mock<IOutputPluginFactory>();

            var context = new Mock<IContext>();
            var source = new Mock<IPlugin>();
            var processPlugin = new Mock<IPlugin>();
            var outputPlugin = new Mock<IPlugin>();

            contextFactory.Setup(c => c.Create()).Returns(context.Object);
            sourceFactory.Setup(s => s.CreateSource()).Returns(source.Object);
            processesFactory.Setup(p => p.Create()).Returns(new List<IPlugin> {processPlugin.Object});
            outputFactory.Setup(o => o.CreateOutput()).Returns(outputPlugin.Object);

            source.Setup(s => s.CanProcess(It.IsAny<IContext>())).Returns(true);
            IContext actualSourceContext = null;
            source.Setup(s => s.Process(It.IsAny<IContext>())).Callback<IContext>(c => actualSourceContext = c);

            processPlugin.Setup(p => p.CanProcess(It.IsAny<IContext>())).Returns(true);
            IContext actualProcessContext = null;
            processPlugin.Setup(s => s.Process(It.IsAny<IContext>())).Callback<IContext>(c => actualProcessContext = c);

            outputPlugin.Setup(p => p.CanProcess(It.IsAny<IContext>())).Returns(true);
            IContext actualOutputContext = null;
            outputPlugin.Setup(s => s.Process(It.IsAny<IContext>())).Callback<IContext>(c => actualOutputContext = c);
            
            var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, processesFactory.Object, outputFactory.Object);

            target.Process();

            Assert.AreEqual(context.Object, actualSourceContext);
            Assert.AreEqual(context.Object, actualProcessContext);
            Assert.AreEqual(context.Object, actualOutputContext);

            contextFactory.VerifyAll();
            sourceFactory.VerifyAll();
            processesFactory.VerifyAll();
            outputFactory.VerifyAll();

            context.VerifyAll();
            source.VerifyAll();
            processPlugin.VerifyAll();
            outputPlugin.VerifyAll();
        }

        [TestMethod]
        public void ProcessSourceFactoryCreateNullResultException()
        {
            var contextFactory = new Mock<IContextFactory>();
            var sourceFactory = new Mock<ISourcePluginFactory>();
            var processesFactory = new Mock<IProcessesPluginFactory>();
            var outputFactory = new Mock<IOutputPluginFactory>();

            var context = new Mock<IContext>();

            contextFactory.Setup(c => c.Create()).Returns(context.Object);
            sourceFactory.Setup(s => s.CreateSource()).Returns((IPlugin) null);

            var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, processesFactory.Object, outputFactory.Object);
            try
            {
                target.Process();
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual("sourceFactory create null value object", exception.Message);
            }
        }

        [TestMethod]
        public void ProcessProcessesFactoryCreateNullResultException()
        {
            var contextFactory = new Mock<IContextFactory>();
            var sourceFactory = new Mock<ISourcePluginFactory>();
            var processesFactory = new Mock<IProcessesPluginFactory>();
            var outputFactory = new Mock<IOutputPluginFactory>();

            var context = new Mock<IContext>();
            var source = new Mock<IPlugin>();

            contextFactory.Setup(c => c.Create()).Returns(context.Object);
            sourceFactory.Setup(s => s.CreateSource()).Returns(source.Object);
            processesFactory.Setup(p => p.Create()).Returns((IEnumerable<IPlugin>) null);
            var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, processesFactory.Object, outputFactory.Object);

            try
            {
                target.Process();
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual("processesFactory create null value object", exception.Message);
            }
        }

        [TestMethod]
        public void ProcessOutputFactoryCreateNullResultException()
        {
            var contextFactory = new Mock<IContextFactory>();
            var sourceFactory = new Mock<ISourcePluginFactory>();
            var processesFactory = new Mock<IProcessesPluginFactory>();
            var outputFactory = new Mock<IOutputPluginFactory>();

            var context = new Mock<IContext>();
            var source = new Mock<IPlugin>();
            var processPlugin = new Mock<IPlugin>();

            contextFactory.Setup(c => c.Create()).Returns(context.Object);
            sourceFactory.Setup(s => s.CreateSource()).Returns(source.Object);
            processesFactory.Setup(p => p.Create()).Returns(new List<IPlugin> { processPlugin.Object });
            outputFactory.Setup(o => o.CreateOutput()).Returns((IPlugin) null);

            var target = new TextWordCalculator(contextFactory.Object, sourceFactory.Object, processesFactory.Object, outputFactory.Object);
            try
            {
                target.Process();
            }
            catch (NullReferenceException exception)
            {
                Assert.AreEqual("outputFactory create null value object", exception.Message);
            }
        }
    }
}