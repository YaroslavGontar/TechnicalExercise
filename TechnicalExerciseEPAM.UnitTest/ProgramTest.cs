using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TechnicalExerciseEPAM.UnitTest
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void NumberOfWordAappearsSimpleSampleResult_this_2_is_2_a_1_statement_1_and_1_so_1()
        {
            var target = new Program();
            var actual = target.NumberOfWordAappears("This is a statement, and so is this.");

            Assert.AreEqual(6, actual.Count);

            Assert.IsTrue(actual.ContainsKey("this"));
            Assert.AreEqual(2, actual["this"]);

            Assert.IsTrue(actual.ContainsKey("is"));
            Assert.AreEqual(2, actual["is"]);

            Assert.IsTrue(actual.ContainsKey("a"));
            Assert.AreEqual(1, actual["a"]);

            Assert.IsTrue(actual.ContainsKey("statement"));
            Assert.AreEqual(1, actual["statement"]);

            Assert.IsTrue(actual.ContainsKey("and"));
            Assert.AreEqual(1, actual["and"]);

            Assert.IsTrue(actual.ContainsKey("so"));
            Assert.AreEqual(1, actual["so"]);
        }

        [TestMethod]
        public void NumberOfWordAappearsEmptyResultEmpty()
        {
            var target = new Program();
            var actual = target.NumberOfWordAappears(string.Empty);

            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void NumberOfWordAappearsNullResultEmpty()
        {
            var target = new Program();
            var actual = target.NumberOfWordAappears(null);

            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void NumberOfWordAappearsWhiteSpacesAndControlSymbolsResultEmpty()
        {
            var target = new Program();
            var actual = target.NumberOfWordAappears("     \n\r       \n\r      \t    ");

            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void NumberOfWordAappearsComplexSampleResult_this_2_1is_1_a_1_1_1_22_2_statement_1_11_1_and_1_so_1_is_1()
        {
            var target = new Program();
            var actual = target.NumberOfWordAappears("This  1is a  1 22 statement, \n\r 22 11 and \n\r so is    this.");

            Assert.AreEqual(10, actual.Count);

            Assert.IsTrue(actual.ContainsKey("this"));
            Assert.AreEqual(2, actual["this"]);

            Assert.IsTrue(actual.ContainsKey("1is"));
            Assert.AreEqual(1, actual["1is"]);

            Assert.IsTrue(actual.ContainsKey("a"));
            Assert.AreEqual(1, actual["a"]);

            Assert.IsTrue(actual.ContainsKey("1"));
            Assert.AreEqual(1, actual["1"]);

            Assert.IsTrue(actual.ContainsKey("22"));
            Assert.AreEqual(2, actual["22"]);

            Assert.IsTrue(actual.ContainsKey("statement"));
            Assert.AreEqual(1, actual["statement"]);

            Assert.IsTrue(actual.ContainsKey("11"));
            Assert.AreEqual(1, actual["11"]);

            Assert.IsTrue(actual.ContainsKey("and"));
            Assert.AreEqual(1, actual["and"]);

            Assert.IsTrue(actual.ContainsKey("so"));
            Assert.AreEqual(1, actual["so"]);

            Assert.IsTrue(actual.ContainsKey("is"));
            Assert.AreEqual(1, actual["is"]);
        }
    }
}
