namespace OCore.Entities
{
    /// <summary>
    /// 用于在数据库中软删除实体，软删除实体并未删除。
    /// 而只是在数据库中将对应的实体标记为IsDeleted = true，被标记为IsDeleted = true的实体不能被应用程序检索。
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 用于将实体标记为“已删除”。
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
