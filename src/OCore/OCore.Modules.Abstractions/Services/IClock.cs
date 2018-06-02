using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Modules
{
    /// <summary>
	/// Provides the current Utc <see cref="DateTime"/>, and time related method for cache management.
	/// This service should be used whenever the current date and time are needed, instead of <seealso cref="DateTime"/> directly.
	/// It also makes implementations more testable, as time can be mocked.
    /// 提供当前的Utc<see cref="DateTime"/>时间，和时间有关的缓存管理相关的方法无论何时需要取当前时间都应当通过些服务来取，而不是直接使用<seealso cref="DateTime"/>来取，
    /// 它使得实现更具可测试性，因为时间可能被伪造
	/// </summary>
	public interface IClock
    {
        /// <summary>
        /// Gets the current <see cref="DateTime"/> of the system, expressed in Utc
        /// </summary>
        DateTime UtcNow { get; }
    }
}
