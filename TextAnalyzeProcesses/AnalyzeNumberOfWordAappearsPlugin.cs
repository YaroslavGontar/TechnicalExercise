using System;
using System.Linq;
using SharedInterfaces;

namespace TextAnalyzeProcesses
{
    public class AnalyzeNumberOfWordAappearsPlugin : IPlugin
    {
        public bool CanProcess(IContext context)
        {
            return context?.Result != null;
        }

        public void Process(IContext context)
        {
            if (!CanProcess(context)) throw new ArgumentException("Check argument with CanProcess method before run Process.");
            context.Source = context.Result;
            var groups = context.Result.ToString().ToLower().Split().GroupBy(w => w);

            context.Result = groups.Where(g => !string.IsNullOrWhiteSpace(g.Key))
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}