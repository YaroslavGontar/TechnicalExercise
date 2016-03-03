using System.Diagnostics;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    public class DebugLogger : ILogger
    {
        public void WriteLog(string message)
        {
            Debug.WriteLine(message);
        }
    }
}