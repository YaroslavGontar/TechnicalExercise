using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedInterfaces;

namespace TextAnalyzeProcesses.UnitTest
{
    [TestClass]
    public class TextFilterPluginTest
    {
        [TestMethod]
        public void CanProcessContextNullResultFalse()
        {
            var target = new TextFilterPlugin();
            var actual = target.CanProcess(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanProcessContextResultNullResultFalse()
        {
            var context = new Mock<IContext>();

            context.SetupGet(c => c.Result).Returns(null);

            var target = new TextFilterPlugin();
            var actual = target.CanProcess(context.Object);

            Assert.IsFalse(actual);
            context.VerifyAll();
        }

        [TestMethod]
        public void CanProcessContextResultNotNullResultTrue()
        {
            var context = new Mock<IContext>();
            var expectedResult = new object();
            context.SetupGet(c => c.Result).Returns(expectedResult);

            var target = new TextFilterPlugin();
            var actual = target.CanProcess(context.Object);

            Assert.IsTrue(actual);
            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextNullResultThrowException()
        {
            var target = new TextFilterPlugin();
            try
            {
                target.Process(null);
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Check argument with CanProcess method before run Process.", exception.Message);
            }

            try
            {
                var context = new Mock<IContext>();
                context.SetupGet(c => c.Result).Returns(null);
                target.Process(context.Object);
            }
            catch (ArgumentException exception)
            {
                Assert.AreEqual("Check argument with CanProcess method before run Process.", exception.Message);
            }

        }

        [TestMethod]
        public void ProcessContextResultEmptyResultEmpty()
        {
            var context = new Mock<IContext>();
            var expectedResult = string.Empty;
            context.SetupGet(c => c.Result).Returns(expectedResult);
            string actualSource = null;
            context.SetupSet(c => c.Source = It.IsAny<string>()).Callback<object>(s => actualSource = s as string);
            string actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<string>()).Callback<object>(s => actualResult = s as string);

            var target = new TextFilterPlugin();
            target.Process(context.Object);

            Assert.AreEqual(string.Empty, actualSource);
            Assert.AreEqual(string.Empty, actualResult);
            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextResultSimpleTestStringResultTestStringWithoutPunctuation()
        {
            var context = new Mock<IContext>();
            context.SetupGet(c => c.Result).Returns(TestConstants.SimpleTestString);
            string actualSource = null;
            context.SetupSet(c => c.Source = It.IsAny<string>()).Callback<object>(s => actualSource = s as string);
            string actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<string>()).Callback<object>(s => actualResult = s as string);

            var target = new TextFilterPlugin();
            target.Process(context.Object);

            Assert.AreEqual(TestConstants.SimpleTestString, actualSource);
            Assert.AreEqual(TestConstants.FiltredSimpleTestString, actualResult);
            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextResultComplexTestStringResultTestStringWithoutPunctuation()
        {
            var context = new Mock<IContext>();
            context.SetupGet(c => c.Result).Returns(TestConstants.ComplexTestString);
            string actualSource = null;
            context.SetupSet(c => c.Source = It.IsAny<string>()).Callback<object>(s => actualSource = s as string);
            string actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<string>()).Callback<object>(s => actualResult = s as string);

            var target = new TextFilterPlugin();
            target.Process(context.Object);

            Assert.AreEqual(TestConstants.ComplexTestString, actualSource);
            Assert.AreEqual(TestConstants.FiltredComplexTestString, actualResult);
            context.VerifyAll();
        }
    }
}