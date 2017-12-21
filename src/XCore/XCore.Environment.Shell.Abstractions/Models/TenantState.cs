using System;
using System.Collections.Generic;
using System.Text;

namespace XCore.Environment.Shell.Models
{
    public enum TenantState
    {
        /// <summary>
        /// 未初始化
        /// </summary>
        Uninitialized,
        /// <summary>
        /// 已初始化
        /// </summary>
        Initializing,
        /// <summary>
        /// 正常、正在运转中
        /// </summary>
        Running,
        /// <summary>
        /// 禁用
        /// </summary>
        Disabled,
        /// <summary>
        /// 无效
        /// </summary>
        Invalid
    }
}
