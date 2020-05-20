using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LSH.Infrastructure.Autofac
{
    public class AutofacProvider
    {
        private IContainer _container;
        private ContainerBuilder _builder;

        public static AutofacProvider Current { get; private set; }

        private AutofacProvider(IServiceCollection services)
        {
            _builder = new ContainerBuilder();
            _builder.Populate(services);
        }

        public static void Init(IServiceCollection services)
        {
            Current = new AutofacProvider(services);

        }


        public void Register<Source, Dest>()
        {
            _builder.RegisterType<Source>().As<Dest>();
        }

        public Dest Resolver<Dest>()
        {
            return _container.Resolve<Dest>();
        }


        public void Register(Type[] sourceTypes, Type[] destTypes)
        {
            foreach (var dest in destTypes)
            {
                foreach (var source in sourceTypes)
                {
                    if (dest.IsAssignableFrom(source))
                    {
                        _builder.RegisterType(source).As(dest);
                        break;
                    }
                }
            }

        }

        public void Register(IEnumerable<string> assemblys)
        {
            foreach (var assembly in assemblys)
            {
                Assembly curAssembly = Assembly.Load(assembly);
                Type[] types = curAssembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsDefined(typeof(RegisterAttribute)))
                    {
                        RegisterAttribute curType = type.GetCustomAttribute<RegisterAttribute>();
                        _builder.RegisterType(type).As(curType.DestType).PropertiesAutowired();
                    }
                    else if (type.Name.EndsWith("Controller"))
                    {
                       // _builder.RegisterType(type).AsSelf().PropertiesAutowired();
                    }
                }
            }


            _builder.RegisterAssemblyTypes(Assembly.Load("Api_Demo"));

        }


        public   IServiceProvider Build()
        {
            _container = _builder.Build();
           return new AutofacServiceProvider(_container);
        }

    }
}
