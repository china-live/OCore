namespace OCore.Environment.Extensions.Features
{
    /// <summary>
    /// 定义扩展模块提供的功能点
    /// </summary>
    public interface IFeatureInfo
    {
        string Id { get; }
        string Name { get; }
        /// <summary>
        /// 优先级
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// 类型
        /// </summary>
        string Category { get; }
        /// <summary>
        /// 描述信息
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 所属的扩展模块
        /// </summary>
        IExtensionInfo Extension { get; }
        /// <summary>
        /// 该功能点依赖的其它扩展模块集合
        /// </summary>
        string[] Dependencies { get; }
    }
}
