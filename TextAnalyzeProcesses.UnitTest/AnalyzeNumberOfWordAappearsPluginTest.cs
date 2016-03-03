using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedInterfaces;

namespace TextAnalyzeProcesses.UnitTest
{
    [TestClass]
    public class AnalyzeNumberOfWordAappearsPluginTest
    {
        [TestMethod]
        public void CanProcessContextNullResultFalse()
        {
            var target = new AnalyzeNumberOfWordAappearsPlugin();
            var actual = target.CanProcess(null);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanProcessContextResultNullResultFalse()
        {
            var context = new Mock<IContext>();

            context.SetupGet(c => c.Result).Returns(null);

            var target = new AnalyzeNumberOfWordAappearsPlugin();
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

            var target = new AnalyzeNumberOfWordAappearsPlugin();
            var actual = target.CanProcess(context.Object);

            Assert.IsTrue(actual);
            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextNullResultThrowException()
        {
            var target = new AnalyzeNumberOfWordAappearsPlugin();
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
            IDictionary<string, int> actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<IDictionary<string, int>>()).Callback<object>(s => actualResult = s as IDictionary<string, int>);

            var target = new AnalyzeNumberOfWordAappearsPlugin();
            target.Process(context.Object);

            Assert.AreEqual(string.Empty, actualSource);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(0, actualResult.Count);

            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextResultSimpleTestStringResult_this_2_is_2_a_1_statement_1_and_1_so_1()
        {
            var context = new Mock<IContext>();
            context.SetupGet(c => c.Result).Returns(TestConstants.FiltredSimpleTestString);
            string actualSource = null;
            context.SetupSet(c => c.Source = It.IsAny<string>()).Callback<object>(s => actualSource = s as string);
            IDictionary<string, int> actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<IDictionary<string, int>>()).Callback<object>(s => actualResult = s as IDictionary<string, int>);

            var target = new AnalyzeNumberOfWordAappearsPlugin();
            target.Process(context.Object);

            Assert.AreEqual(TestConstants.FiltredSimpleTestString, actualSource);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(6, actualResult.Count);

            Assert.IsTrue(actualResult.ContainsKey("this"));
            Assert.AreEqual(2, actualResult["this"]);

            Assert.IsTrue(actualResult.ContainsKey("is"));
            Assert.AreEqual(2, actualResult["is"]);

            Assert.IsTrue(actualResult.ContainsKey("a"));
            Assert.AreEqual(1, actualResult["a"]);

            Assert.IsTrue(actualResult.ContainsKey("statement"));
            Assert.AreEqual(1, actualResult["statement"]);

            Assert.IsTrue(actualResult.ContainsKey("and"));
            Assert.AreEqual(1, actualResult["and"]);

            Assert.IsTrue(actualResult.ContainsKey("so"));
            Assert.AreEqual(1, actualResult["so"]);

            context.VerifyAll();
        }

        [TestMethod]
        public void ProcessContextResultComplexTestStringResult_this_2_1is_1_a_1_1_1_22_2_statement_1_11_1_and_1_so_1_is_1()
        {
            var context = new Mock<IContext>();
            context.SetupGet(c => c.Result).Returns(TestConstants.FiltredComplexTestString);
            string actualSource = null;
            context.SetupSet(c => c.Source = It.IsAny<string>()).Callback<object>(s => actualSource = s as string);
            IDictionary<string, int> actualResult = null;
            context.SetupSet(c => c.Result = It.IsAny<IDictionary<string, int>>()).Callback<object>(s => actualResult = s as IDictionary<string, int>);

            var target = new AnalyzeNumberOfWordAappearsPlugin();
            target.Process(context.Object);

            Assert.AreEqual(TestConstants.FiltredComplexTestString, actualSource);
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(10, actualResult.Count);

            Assert.IsTrue(actualResult.ContainsKey("this"));
            Assert.AreEqual(2, actualResult["this"]);

            Assert.IsTrue(actualResult.ContainsKey("1is"));
            Assert.AreEqual(1, actualResult["1is"]);

            Assert.IsTrue(actualResult.ContainsKey("a"));
            Assert.AreEqual(1, actualResult["a"]);

            Assert.IsTrue(actualResult.ContainsKey("1"));
            Assert.AreEqual(1, actualResult["1"]);

            Assert.IsTrue(actualResult.ContainsKey("22"));
            Assert.AreEqual(2, actualResult["22"]);

            Assert.IsTrue(actualResult.ContainsKey("statement"));
            Assert.AreEqual(1, actualResult["statement"]);

            Assert.IsTrue(actualResult.ContainsKey("11"));
            Assert.AreEqual(1, actualResult["11"]);

            Assert.IsTrue(actualResult.ContainsKey("and"));
            Assert.AreEqual(1, actualResult["and"]);

            Assert.IsTrue(actualResult.ContainsKey("so"));
            Assert.AreEqual(1, actualResult["so"]);

            Assert.IsTrue(actualResult.ContainsKey("is"));
            Assert.AreEqual(1, actualResult["is"]);

            context.VerifyAll();
        }
    }
}