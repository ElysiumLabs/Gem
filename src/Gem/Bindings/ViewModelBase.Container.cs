using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gem.Bindings
{
    // commented to not influence to antipattern
    //public partial class ViewModelBase : IContainerProvider
    //{
    //    public ViewModelBase(PrismContainerExtension prismContainerExtension)
    //    {
    //        PrismContainerExtension = prismContainerExtension;

    //        ResolveDefaultServicesInternal();
    //    }

    //    private void CheckThrowNullContainer()
    //    {
    //        if (PrismContainerExtension == null) { throw new Exception("PrismContainerExtension is null"); }
    //    }

    //    public object Resolve(Type type)
    //    {
    //        CheckThrowNullContainer();
    //        return PrismContainerExtension.Resolve(type);
    //    }

    //    public object Resolve(Type type, params (Type Type, object Instance)[] parameters)
    //    {
    //        CheckThrowNullContainer();
    //        return PrismContainerExtension.Resolve(type, parameters);
    //    }

    //    public object Resolve(Type type, string name)
    //    {
    //        CheckThrowNullContainer();
    //        return PrismContainerExtension.Resolve(type, name);
    //    }

    //    public object Resolve(Type type, string name, params (Type Type, object Instance)[] parameters)
    //    {
    //        CheckThrowNullContainer();
    //        return PrismContainerExtension.Resolve(type, name, parameters);
    //    }

    //    public T Resolve<T>()
    //    {
    //        return (T)Resolve(typeof(T));
    //    }

    //    public T Resolve<T>(params (Type Type, object Instance)[] parameters)
    //    {
    //        return (T)Resolve(typeof(T), parameters);
    //    }

    //    public T Resolve<T>(string name)
    //    {
    //        return (T)Resolve(typeof(T), name);
    //    }

    //    public T Resolve<T>(string name, params (Type Type, object Instance)[] parameters)
    //    {
    //        return (T)Resolve(typeof(T), name, parameters);
    //    }

    //    public PrismContainerExtension PrismContainerExtension { get; }
    //}
}
