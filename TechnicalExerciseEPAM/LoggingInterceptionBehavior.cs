using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;
using SharedInterfaces;

namespace TechnicalExerciseEPAM
{
    public class LoggingInterceptionBehavior : IInterceptionBehavior
    {
        private ILogger _logger;

        public LoggingInterceptionBehavior(ILogger logger)
        {
            if(logger == null) throw new ArgumentNullException("logger");
            _logger = logger;
        }

        public IMethodReturn Invoke(IMethodInvocation input,
            GetNextInterceptionBehaviorDelegate getNext)
        {
            var startTime = DateTime.Now;

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            if (input.MethodBase.Name == "Process")
            {
                if (result.Exception != null)
                {
                    WriteLog(String.Format(
                        "[{3}]: {0}.{1} threw exception {2}",
                        input.Target.GetType().Name, input.MethodBase.Name, result.Exception.Message,
                        DateTime.Now.ToLongTimeString()));
                }
                else
                {
                    WriteLog(String.Format(
                        "[{3}]: {0}.{1} executed - {2} ms.",
                        input.Target.GetType().Name, input.MethodBase.Name, (DateTime.Now - startTime).TotalMilliseconds,
                        DateTime.Now.ToLongTimeString()));
                }
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        private void WriteLog(string message)
        {
            _logger.WriteLog(message);
        }
    }
}