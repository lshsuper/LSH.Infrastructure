using Autofac;
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

        private AutofacProvider()
        {
            _builder = new ContainerBuilder();
            _container = _builder.Build();
        }

        public static void Init()
        {
            Current = new AutofacProvider();
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
                        _builder.RegisterType(type).As(curType.DestType);
                    }
                }
            }

        }

    }
}
