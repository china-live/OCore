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

        /// <summary>
        /// Returns the list of all available <see cref="ITimeZone" />.
        /// </summary>
        ITimeZone[] GetTimeZones();

        /// <summary>
        /// Returns a <see cref="ITimeZone" /> from a time zone id or the local system's one if not found.
        /// </summary>
        ITimeZone GetTimeZone(string timeZoneId);

        /// <summary>
        /// Returns a default <see cref="ITimeZone" /> for the system.
        /// </summary>
        ITimeZone GetSystemTimeZone();

        /// <summary>
        /// Converts a <see cref="DateTimeOffset" /> to the specified <see cref="ITimeZone" /> instance.
        /// </summary>
        DateTimeOffset ConvertToTimeZone(DateTimeOffset dateTimeOffset, ITimeZone timeZone);
    }
}
