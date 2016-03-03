using System.Collections.Generic;

namespace SharedInterfaces
{
    public interface IParameters
    {
        IEnumerable<string> Parameters { get; }
    }
}