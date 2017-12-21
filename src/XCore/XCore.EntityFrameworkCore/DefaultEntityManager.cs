using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XCore.Entities;
using XCore.Environment.Extensions;
using XCore.Linq.Expressions;

namespace XCore.EntityFrameworkCore
{
    public class DefaultEntityManager : IEntityManager
    {
        private readonly IExtensionManager _extensionManager;
        private List<Type> Types = new List<Type>();

        public DefaultEntityManager(IExtensionManager extensionManager)
        {
            _extensionManager = extensionManager;
            var extensionAssemblys = GetAssemblysOfExtension();
            foreach (var extension in extensionAssemblys)
            {
                Types.AddRange(过滤(extension.GetTypes()));

                LoadReferencedAssemblys(extension.GetReferencedAssemblies());
            }
        }

        public virtual IEnumerable<Type> GetEntitys()
        {
            return Types.Where(type => type.GetCustomAttribute(typeof(EntityAttribute)) != null);
        }

        public virtual IEnumerable<Type> GetEntityTypeConfiguration()
        {
            return Types.Where(type => type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0);
        }

        private IEnumerable<Assembly> GetAssemblysOfExtension()
        {
            var extensionAccemblys = new List<Assembly>();
            var extensions = _extensionManager.GetExtensions();
            foreach (var item in extensions)
            {
                extensionAccemblys.Add(_extensionManager.LoadExtensionAsync(item).Result.Assembly);
            }
            return extensionAccemblys;
        }

        private void LoadReferencedAssemblys(AssemblyName[] assenblyNames)
        {
            var extensionAccemblys = new List<Assembly>();
            foreach (var item in assenblyNames)
            {
                var assembly = Assembly.Load(item);

                if (assembly != null)
                {
                    //extensionAccemblys.Add(assembly);
                    Types.AddRange(过滤(assembly.GetTypes()));
                }
            }
        }

        private IEnumerable<Type> 过滤(IEnumerable<Type> types) {
            return types.Where(type =>
                !String.IsNullOrEmpty(type.Namespace)
                && type.IsClass
                && type.IsPublic
                && !type.IsAbstract);
        }
    }

    public class MigrationEntityManager : IEntityManager
    {
        private List<Type> entityTypes = new List<Type>();

        public MigrationEntityManager()
        {

        }

        public virtual IEnumerable<Type> GetEntitys()
        {
            return entityTypes.Where(type =>
                !String.IsNullOrEmpty(type.Namespace)
                && type.IsClass
                && type.IsPublic
                && !type.IsAbstract
                && type.GetCustomAttribute(typeof(EntityAttribute)) != null);
        }

        public virtual IEnumerable<Type> GetEntityTypeConfiguration()
        {
            return entityTypes.Where(type =>
            !String.IsNullOrEmpty(type.Namespace)
            && type.IsClass
            && type.IsPublic
            && !type.IsAbstract
            && type.GetInterfaces().Where(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)).Count() > 0);
        }

        public void LoadAssemblys(params string[] entityAssenblyNames)
        {
            var extensionAccemblys = new List<Assembly>();
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
