using System;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    public class ConsoleLogger : ILogger
    {
        public void WriteLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}