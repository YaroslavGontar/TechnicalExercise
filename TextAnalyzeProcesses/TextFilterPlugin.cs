using System;
using System.Linq;
using SharedInterfaces;

namespace TextAnalyzeProcesses
{
    public class TextFilterPlugin : IPlugin
    {
        public bool CanProcess(IContext context)
        {
            return context?.Result != null;
        }

        public void Process(IContext context)
        {
            if (!CanProcess(context)) throw new ArgumentException("Check argument with CanProcess method before run Process.");

            context.Source = context.Result;
            var filtred =
                context.Result.ToString().ToCharArray().Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));

            context.Result = new string(filtred.ToArray());
        }
    }
}