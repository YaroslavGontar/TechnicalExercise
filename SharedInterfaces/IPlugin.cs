namespace SharedInterfaces
{
    public interface IPlugin
    {
        bool CanProcess(IContext context);
        void Process(IContext context);
    }
}
