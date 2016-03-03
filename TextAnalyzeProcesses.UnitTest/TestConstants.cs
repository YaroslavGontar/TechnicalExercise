namespace TextAnalyzeProcesses.UnitTest
{
    public static class TestConstants
    {
        public static string SimpleTestString = "This is a statement, and so is this.";
        public static string FiltredSimpleTestString = "This is a statement and so is this";

        public static string ComplexTestString = "This  1is, a  1 22 statement, \n\r 22 11... and \n\r so is    this.";
        public static string FiltredComplexTestString = "This  1is a  1 22 statement \n\r 22 11 and \n\r so is    this";
    }
}