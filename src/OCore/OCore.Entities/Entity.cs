using System;

namespace OCore.Entities
{
    public interface IEntity
    {
        //JObject Properties { get; }
    }

    public abstract class Entity : IEntity
    {
        //public JObject Properties { get; set; } = new JObject();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
    }

    //public static class EntityExtensions
    //{
    //    /// <summary>
    //    /// Extracts the specified type of property.
    //    /// </summary>
    //    /// <typeparam name="T">The type of the property to extract.</typeparam>
    //    /// <returns>The default value of the requested type if the property was not found.</returns>
    //    public static T As<T>(this IEntity entity)
    //    {
    //        var typeName = typeof(T).Name;
    //        return entity.As<T>(typeName);
    //    }

    //    /// <summary>
    //    /// Extracts the specified named property.
    //    /// </summary>
    //    /// <typeparam name="T">The type of the property to extract.</typeparam>
    //    /// <param name="name">The name of the property to extract.</param>
    //    /// <returns>The default value of the requested type if the property was not found.</returns>
    //    public static T As<T>(this IEntity entity, string name)
    //    {
    //        JToken value;

    //        if (entity.Properties.TryGetValue(name, out value))
    //        {
    //            return value.ToObject<T>();
    //        }

    //        return default(T);
    //    }

    //    public static IEntity Put<T>(this IEntity entity, T aspect) where T : new()
    //    {
    //        return entity.Put(typeof(T).Name, aspect);
    //    }

    //    public static IEntity Put(this IEntity entity, string name, object property)
    //    {
    //        entity.Properties[name] = JObject.FromObject(property);
    //        return entity;
    //    }

    //    /// <summary>
    //    /// Modifies or create an aspect.
    //    /// </summary>
    //    /// <typeparam name="name">The name of the aspect.</typeparam>
    //    /// <typeparam name="action">An action to apply on the aspect.</typeparam>
    //    /// <returns>The current <see cref="IEntity"/> instance.</returns>
    //    public static IEntity Alter<TAspect>(this IEntity entity, string name, Action<TAspect> action) where TAspect : new()
    //    {
    //        JToken value;
    //        TAspect obj;

    //        if (!entity.Properties.TryGetValue(name, out value))
    //        {
    //            obj = new TAspect();
    //        }
    //        else
    //        {
    //            obj = value.ToObject<TAspect>();
    //        }

    //        action?.Invoke(obj);

    //        entity.Put(name, obj);

    //        return entity;
    //    }
    //}

    ///// <summary>
    ///// Basic implementation of IEntity interface.
    ///// An entity can inherit this class of directly implement to IEntity interface.
    ///// </summary>
    ///// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    //[Serializable]
    //public abstract partial class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    //{
    //    /// <summary>
    //    /// Unique identifier for this entity.
    //    /// </summary>
    //    public virtual TPrimaryKey Id { get; set; }

    //    /// <summary>
    //    /// Checks if this entity is transient (it has not an Id).
    //    /// </summary>
    //    /// <returns>True, if this entity is transient</returns>
    //    public virtual bool IsTransient()
    //    {
    //        if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
    //        {
    //            return true;
    //        }

    //        //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
    //        if (typeof(TPrimaryKey) == typeof(int))
    //        {
    //            return Convert.ToInt32(Id) <= 0;
    //        }

    //        if (typeof(TPrimaryKey) == typeof(long))
    //        {
    //            return Convert.ToInt64(Id) <= 0;
    //        }

    //        return false;
    //    }

    //    /// <inheritdoc/>
    //    public override bool Equals(object obj)
    //    {
    //        if (obj == null || !(obj is Entity<TPrimaryKey>))
    //        {
    //            return false;
    //        }

    //        //Same instances must be considered as equal
    //        if (ReferenceEquals(this, obj))
    //        {
    //            return true;
    //        }

    //        //Transient objects are not considered as equal
    //        var other = (Entity<TPrimaryKey>)obj;
    //        if (IsTransient() && other.IsTransient())
    //        {
    //            return false;
    //        }

    //        //Must have a IS-A relation of types or must be same type
    //        var typeOfThis = GetType();
    //        var typeOfOther = other.GetType();
    //        if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
    //        {
    //            return false;
    //        }

    //        return Id.Equals(other.Id);
    //    }

    //    /// <inheritdoc/>
    //    public override int GetHashCode()
    //    {
    //        if (Id == null)
    //        {
    //            return 0;
    //        }

    //        return Id.GetHashCode();
    //    }

    //    /// <inheritdoc/>
    //    public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
    //    {
    //        if (Equals(left, null))
    //        {
    //            return Equals(right, null);
    //        }

    //        return left.Equals(right);
    //    }

    //    /// <inheritdoc/>
    //    public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
    //    {
    //        return !(left == right);
    //    }

    //    /// <inheritdoc/>
    //    public override string ToString()
    //    {
    //        return $"[{GetType().Name} {Id}]";
    //    }
    //}

    ///// <summary>
    ///// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    ///// </summary>
    //[Serializable]
    //public abstract partial class Entity : Entity<int>, IEntity
    //{

    //}

    ///// <summary>
    ///// 主键类型为long的实体定义接口
    ///// </summary>
    //[Serializable]
    //public abstract partial class EntityOfLong : Entity<long>, IEntityOfLong
    //{

    //}

    ///// <summary>
    ///// 主键类型为String的实体定义接口
    ///// </summary>
    //[Serializable]
    //public abstract partial class EntityOfString : Entity<string>, IEntityOfString
    //{
    //}
}
