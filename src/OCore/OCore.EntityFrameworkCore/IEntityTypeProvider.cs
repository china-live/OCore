using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OCore.Entities;
using OCore.Environment.Extensions;

namespace OCore.EntityFrameworkCore
{
    /// <summary>
    /// 实现该接口用于提供当前应用程序内定义的所有实体及映射关系集合
    /// </summary>
    public interface IEntityTypeProvider
    {
        IEnumerable<Type> GetEntityTypes();
        IEnumerable<Type> GetEntityTypeConfigurations();
    }

    /// <summary>
    /// 提供当前应用程序加载的所有扩展模块中定义的实体及映射关系集合
    /// </summary>
    public class DefaultEntityTypeProvider : IEntityTypeProvider
    {
        private readonly IExtensionManager _extensionManager;
        private List<Type> Types = new List<Type>();

        private Func<Type, bool> predicate = type => !String.IsNullOrEmpty(type.Namespace)
                && type.IsClass
                && type.IsPublic
                && !type.IsAbstract;

        public DefaultEntityTypeProvider(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
            foreach (var item in _extensionManager.GetExtensions())
            {
                var extension = _extensionManager.LoadExtensionAsync(item).Result.Assembly;

                Types.AddRange(extension.GetTypes().Where(predicate));

                foreach (var assemblie in extension.GetReferencedAssemblies())
                {
                    var assembly = Assembly.Load(assemblie);
                    if (assembly != null)
                    {
                        //extensionAccemblys.Add(assembly);
                        Types.AddRange(assembly.GetTypes().Where(predicate));
                    }
                }
            }

            //var extensionAssemblys = GetAssemblysOfExtension();
            //foreach (var extension in extensionAssemblys)
            //{
            //    Types.AddRange(过滤(extension.GetTypes()));

            //    LoadReferencedAssemblys(extension.GetReferencedAssemblies());
            //}
        }

        public virtual IEnumerable<Type> GetEntityTypes()
        {
            return Types.Where(type => type.GetCustomAttribute(typeof(EntityAttribute)) != null).Distinct().ToList();
        }

        public virtual IEnumerable<Type> GetEntityTypeConfigurations()
        {
            //return Types.Where(type => (type.BaseType?.IsGenericType ?? false) 
            //&& (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>) /*|| type.BaseType.GetGenericTypeDefinition() == typeof(QueryTypeConfiguration<>)*/)
            //    /*type => type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0*/);


            return Types.Where(type => type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0).Distinct().ToList();
        }
    }

    /// <summary>
    /// 提供指定的扩展模块中定义的实体及映射关系集合
    /// </summary>
    public class MigrationEntityTypeProvider : IEntityTypeProvider
    {
        private List<Type> entityTypes = new List<Type>();
        private Func<Type, bool> predicate = type => !String.IsNullOrEmpty(type.Namespace)
                && type.IsClass
                && type.IsPublic
                && !type.IsAbstract;

        public MigrationEntityTypeProvider()
        {
        }
        public MigrationEntityTypeProvider(params string[] entityAssenblyNames)
        {
            AddAssemblys(entityAssenblyNames);
        }

        public virtual IEnumerable<Type> GetEntityTypes()
        {
            return entityTypes.Where(predicate).Where(type => type.GetCustomAttribute(typeof(EntityAttribute)) != null).Distinct().ToList();
        }

        public virtual IEnumerable<Type> GetEntityTypeConfigurations()
        {
            //return entityTypes.Where(predicate).Where(type => (type.BaseType?.IsGenericType ?? false)
            //&& (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>) /*|| type.BaseType.GetGenericTypeDefinition() == typeof(QueryTypeConfiguration<>)*/)
            // /*&& type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0*/);
            return entityTypes.Where(predicate).Where(type => type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0).Distinct().ToList();
        }

        public void AddAssemblys(params string[] entityAssenblyNames)
        {
            //var extensionAccemblys = new List<Assembly>();
            foreach (var item in entityAssenblyNames)
            {
                var assembly = Assembly.Load(new AssemblyName(item));

                if (assembly != null)
                {
                    //extensionAccemblys.Add(assembly);
                    entityTypes.AddRange(assembly.GetTypes());
                }
            }
        }
    }
}
