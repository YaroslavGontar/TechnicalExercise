using System;
using SharedInterfaces;

namespace SourceFromParameters
{
    public class SourceFromParametersPlugin : IPlugin
    {
        private readonly IParameters _parameters;

        public SourceFromParametersPlugin(IParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            _parameters = parameters;
        }

        public bool CanProcess(IContext context)
        {
            return context != null;
        }

        public void Process(IContext context)
        {
            if (!CanProcess(context)) throw new ArgumentException("Check argument with CanProcess method before run Process.");
            context.Source = null;
            context.Result = _parameters.Parameters == null ? string.Empty : string.Join(" ", _parameters.Parameters);
        }
    }
}