using System;
using System.Threading.Tasks;

namespace OCore.Modules
{
    public class TimeZoneSelectorResult
    {
        public int Priority { get; set; }
        public Func<Task<string>> TimeZoneId { get; set; }
    }
}
