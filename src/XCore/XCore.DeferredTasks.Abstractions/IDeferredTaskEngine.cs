using System;
using System.Threading.Tasks;

namespace XCore.DeferredTasks
{
    /// <summary>
    /// An implementation of this interface provides a way to enlist custom actions that
    /// will be executed once the request is done. Each action receives a <see cref="DeferredTaskContext"/>.
    /// Actions are executed in a new <see cref="IServiceProvider"/> scope.
    /// 循环执行每一个<see cref="DeferredTaskContext"/>的任务列表。
    /// </summary>
    public interface IDeferredTaskEngine
    {
        /// <summary>
        /// 指示当前是否具有待执行的任务
        /// </summary>
        bool HasPendingTasks { get; }

        /// <summary>
        /// 将新的任务添加到待执行列表
        /// </summary>
        /// <param name="task"></param>
        void AddTask(Func<DeferredTaskContext, Task> task);
 
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context">要执行任务的上下文</param>
        /// <returns></returns>
        Task ExecuteTasksAsync(DeferredTaskContext context);
    }
}
