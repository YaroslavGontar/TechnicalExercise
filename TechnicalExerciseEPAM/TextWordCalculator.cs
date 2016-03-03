using System;
using System.Linq;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    public class TextWordCalculator
    {
        private readonly IContextFactory _contextFactory;
        private readonly ISourcePluginFactory _sourceFactory;
        private readonly IProcessesPluginFactory _processesFactory;
        private readonly IOutputPluginFactory _outputFactory;

        public TextWordCalculator(IContextFactory contextFactory, ISourcePluginFactory sourceFactory,
            IProcessesPluginFactory processesFactory, IOutputPluginFactory outputFactory)
        {
            if (contextFactory == null) throw new ArgumentNullException("contextFactory");
            if (sourceFactory == null) throw new ArgumentNullException("sourceFactory");
            if (processesFactory == null) throw new ArgumentNullException("processesFactory");
            if (outputFactory == null) throw new ArgumentNullException("outputFactory");
            _contextFactory = contextFactory;
            _sourceFactory = sourceFactory;
            _processesFactory = processesFactory;
            _outputFactory = outputFactory;
        }

        public void Process()
        {
            var context = _contextFactory.Create();
            var source = _sourceFactory.CreateSource();
            if (source == null) throw new NullReferenceException("sourceFactory create null value object");
            if (source.CanProcess(context)) source.Process(context);

            var processes = _processesFactory.Create();
            if (processes == null) throw new NullReferenceException("processesFactory create null value object");
            foreach (var process in processes.Where(process => process.CanProcess(context)))
            {
                process.Process(context);
            }

            var output = _outputFactory.CreateOutput();
            if (output == null) throw new NullReferenceException("outputFactory create null value object");
            if (output.CanProcess(context)) output.Process(context);
        }
    }
}