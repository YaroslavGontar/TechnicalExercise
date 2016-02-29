using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalExerciseEPAM
{
    public class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            var numberOfWords = program.NumberOfWordAappears(string.Join("", args));
            foreach (var item in numberOfWords)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
            Console.ReadLine();
        }

        public IDictionary<string, int> NumberOfWordAappears(string sentence)
        {
            if(string.IsNullOrEmpty(sentence)) return new Dictionary<string, int>();

            var filtred = sentence.ToCharArray().Where(c => (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)));
            var groups = new string(filtred.ToArray()).ToLower().Split().GroupBy(w => w);

            return groups.Where(g => !string.IsNullOrWhiteSpace(g.Key)).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
