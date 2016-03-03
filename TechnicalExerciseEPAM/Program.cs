using System;
using Microsoft.Practices.Unity;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer()
                    .InitInterception()
                    .RegisterInstance<ILogger>(new ConsoleLogger())
                    //.RegisterInstance<ILogger>(new DebugLogger()) // Uncomment this and comment ConsoleLogger to switch Logger to debug 
                    .RegisterInstance<IParameters>(new CommandLineArguments(args))
                    .RegisterPlugins()
                    .RegisterFactories();

            var wordCalc = container.Resolve<TextWordCalculator>();

            wordCalc.Process();

            Console.ReadLine();
        }
    }
}
