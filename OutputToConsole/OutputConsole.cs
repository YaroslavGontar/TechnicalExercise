using System;
using System.Collections.Generic;
using SharedInterfaces;

namespace OutputToConsole
{
    public class OutputConsole : IPlugin
    {
        public bool CanProcess(IContext context)
        {
            var dict = context.Result as IDictionary<string, int>;
            return dict != null;
        }

        public void Process(IContext context)
        {
            var numberOfWords = context.Result as IDictionary<string, int>;
            foreach (var item in numberOfWords)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
        }
    }
}