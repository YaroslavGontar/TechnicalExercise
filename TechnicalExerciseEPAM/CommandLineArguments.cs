using System.Collections.Generic;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    public class CommandLineArguments : IParameters
    {
        public CommandLineArguments(string[] args)
        {
            Parameters = args;
        }

        public IEnumerable<string> Parameters { get; private set; }
    }
}