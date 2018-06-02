using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCore.DeferredTasks
{
    /// <summary>
    /// An implementation of this interface is responsible for storing actions that need to be executed
    /// at then end of the active request.
    /// 实现此接口存储需要延迟执行的动作。
    /// </summary>
    public interface IDeferredTaskState
    {
        IList<Func<DeferredTaskContext, Task>> Tasks { get; }
    }
}
