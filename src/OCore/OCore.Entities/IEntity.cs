using System;
using System.Collections.Generic;
using System.Text;

namespace OCore.Entities
{
    ///// <summary>
    ///// 定义实体接口，程序中所有使用Domain的实体都需要直接或间接实现该接口。
    ///// 如果不需要使用Domain相关的或以(Repository)
    ///// </summary>
    ///// <typeparam name="TKey">实体的主键Id</typeparam>
    //public interface IEntity<TKey>//:IEntity
    //{
    //    TKey Id { get; set; }

    //    /// <summary>
    //    /// 检查这个实体是否是临时的（还没有持久化保存到数据库的，此时主键<see cref="Id"/>是默认值（就算被赋值无意义，会被忽略）).
    //    /// </summary>
    //    /// <returns>True:该实体还未持久化保存，False:已持久化保存</returns>
    //    bool IsTransient();
    //}

    ///// <summary>
    ///// 主键类型为Int的实体定义接口
    ///// </summary>
    //public interface IEntity : IEntity<int>
    //{

    //}

    ///// <summary>
    ///// 主键类型为long的实体定义接口
    ///// </summary>
    //public interface IEntityOfLong : IEntity<long>
    //{

    //}

    ///// <summary>
    ///// 主键类型为String的实体定义接口
    ///// </summary>
    //public interface IEntityOfString : IEntity<string>
    //{
    //}

}
