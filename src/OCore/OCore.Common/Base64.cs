using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Common
{
    public sealed class Base64
    {
        /// <summary>
        /// 编码(UTF8)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Encode(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// 解码(UTF8)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Decode(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            try
            {
                byte[] outputb = Convert.FromBase64String(data);
                string result = System.Text.Encoding.UTF8.GetString(outputb);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
    }
}
