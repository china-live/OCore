using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OCore.Environment.Shell.Builders
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Creates a child container.创建子容器
        /// </summary>
        /// <param name="serviceProvider">The service provider to create a child container for.</param>
        /// <param name="serviceCollection">The services to clone.</param>
        public static IServiceCollection CreateChildContainer(this IServiceProvider serviceProvider, IServiceCollection serviceCollection)
        {
            IServiceCollection clonedCollection = new ServiceCollection();
            var servicesByType = serviceCollection.GroupBy(s => s.ServiceType);

            //var xxx = serviceProvider.ToString();

            foreach (var services in servicesByType)
            {
                if (services.Count() == 1)
                {
                    var service = services.First();
                    // Register the singleton instances to all containers
                    if (service.Lifetime == ServiceLifetime.Singleton)
                    {
                        var serviceTypeInfo = service.ServiceType.GetTypeInfo();

                        // Treat open-generic registrations differently
                        if (serviceTypeInfo.IsGenericType /*是否为泛型类型*/ && serviceTypeInfo.GenericTypeArguments.Length == 0)
                        {
                            // There is no Func based way to register an open-generic type, instead of
                            // tenantServiceCollection.AddSingleton(typeof(IEnumerable<>), typeof(List<>));
                            // Right now, we regsiter them as singleton per cloned scope even though it's wrong
                            // but in the actual examples it won't matter.
                            clonedCollection.AddSingleton(service.ServiceType, service.ImplementationType);
                        }
                        else
                        {
                            //当从主容器中的服务解析时，只需将其实例添加到容器中。
                            //它将由所有租户服务提供者共享。

                            // When a service from the main container is resolved, just add its instance to the container.
                            // It will be shared by all tenant service providers.
                            clonedCollection.AddSingleton(service.ServiceType, serviceProvider.GetService(service.ServiceType));

                            // Ideally the service should be resolved when first requested, but ASp.NET DI will call Dispose()
                            // and this would fail reusability of the instance across tenants' containers.
                            //clonedCollection.AddSingleton(service.ServiceType, sp => serviceProvider.GetService(service.ServiceType));
                        }
                    }
                    else
                    {
                        clonedCollection.Add(service);
                    }
                }
                else {
                    // If all services of the same type are not singletons.
                    if (services.All(s => s.Lifetime != ServiceLifetime.Singleton))
                    {
                        // We don't need to resolve them.
                        foreach (var service in services)
                        {
                            clonedCollection.Add(service);
                        }
                    }

                    // If all services of the same type are singletons.
                    else if (services.All(s => s.Lifetime == ServiceLifetime.Singleton))
                    {
                        // We can resolve them from the main container.
                        var instances = serviceProvider.GetServices(services.Key);

                        foreach (var instance in instances)
                        {
                            clonedCollection.AddSingleton(services.Key, instance);
                        }
                    }

                    // If singletons and scoped services are mixed.
                    else
                    {
                        // We need a service scope to resolve them.
                        using (var scope = serviceProvider.CreateScope())
                        {
                            var instances = scope.ServiceProvider.GetServices(services.Key);

                            // Then we only keep singleton instances.
                            for (var i = 0; i < services.Count(); i++)
                            {
                                if (services.ElementAt(i).Lifetime == ServiceLifetime.Singleton)
                                {
                                    clonedCollection.AddSingleton(services.Key, instances.ElementAt(i));
                                }
                                else
                                {
                                    clonedCollection.Add(services.ElementAt(i));
                                }
                            }
                        }
                    }
                }
            }

            return clonedCollection;
        }
    }
}
