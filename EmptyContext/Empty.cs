using SharedInterfaces;

namespace EmptyContext
{
    public class Empty : IContext
    {
        public object Source { get; set; }
        public object Result { get; set; }
    }
}