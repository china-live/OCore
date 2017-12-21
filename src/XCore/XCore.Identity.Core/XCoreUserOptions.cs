namespace XCore.Identity
{
    public class XCoreUserOptions
    {
        /// <summary>
        /// 开启用户激活功能，开启后用户登录需要验证 IsActive 字段，未经激活的用户将会拒绝登录。
        /// </summary>
        public bool RequireActive { get; set; }

        /// <summary>
        /// 是否要求必须提供手机号
        /// </summary>
        public bool RequirePhoneNumber { get; set; }
        /// <summary>
        /// 是否要求必须提供真实姓名
        /// </summary>
        public bool RequireFullName { get; set; }

        /// <summary>
        /// 要求手机号码唯一
        /// </summary>
        public bool RequireUniquePhoneNumber { get; set; }
        /// <summary>
        /// 要求用户姓名唯一
        /// </summary>
        public bool RequireUniqueFullName { get; set; }
    }
}
