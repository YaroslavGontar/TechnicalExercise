using SharedInterfaces;

namespace EmptyContext
{
    public class EmptyContextFactory : IContextFactory
    {
        private readonly IContext _contexts;

        public EmptyContextFactory(IContext contexts)
        {
            _contexts = contexts;
        }

        public IContext Create()
        {
            return _contexts;
        }
    }
}
