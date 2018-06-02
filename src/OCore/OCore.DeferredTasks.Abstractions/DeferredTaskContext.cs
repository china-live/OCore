using System;

namespace OCore.DeferredTasks
{
    public class DeferredTaskContext
    {
        public DeferredTaskContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}
