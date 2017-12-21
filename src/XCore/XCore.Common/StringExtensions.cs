using System;
using System.Collections.Generic;
using System.Linq;

namespace XCore.Common
{
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether this string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// indicates whether this string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }

    public class DtoError
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }

 
    public class DtoResult
    {
        private static readonly DtoResult _success = new DtoResult { Succeeded = true };
        private List<DtoError> _errors = new List<DtoError>();
 
        public bool Succeeded { get; protected set; }
 
        public IEnumerable<DtoError> Errors => _errors;

 
        public static DtoResult Success => _success;


        public static DtoResult Failed(params DtoError[] errors)
        {
            var result = new DtoResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }

        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
