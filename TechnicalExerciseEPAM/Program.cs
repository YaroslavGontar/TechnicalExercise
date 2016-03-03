using Microsoft.Practices.Unity;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer()
                    .RegisterInstance<IParameters>(new CommandLineArguments(args))
                    .RegisterPlugins()
                    .RegisterFactories();

            var wordCalc = container.Resolve<TextWordCalculator>();

            wordCalc.Process();
        }
    }
}
