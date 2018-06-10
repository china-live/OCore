namespace OCore.Entities
{
    /// <summary>
    /// 该接口用于使实体成为 激活/禁用 状态。
    /// </summary>
    public interface IPassivable
    {
        /// <summary>
        /// True: 使该实体处于激活状态
        /// False: 使该实体处于非激活（禁用）状态
        /// </summary>
        bool IsActive { get; set; }
    }
}