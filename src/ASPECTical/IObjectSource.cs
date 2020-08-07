using System;
using System.Collections.Generic;
using ASPECTical.Injection;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public interface IObjectSource
    {
        void WithGlobalInterceptor(IInterceptor interceptor);
        void WithAttributedInterceptor(AttributedInterceptor interceptor);
        T Create<T>(Type t = null);
        T Create<T>(T real) where T : class;
        T Create<T>(Func<T> creationMethod) where T : class;
        object CreateFromReal(object real);
        List<IInterceptor> GlobalInterceptors { get; }
        List<IInterceptor> AttributedInterceptors { get; }
        List<IObjectCreationInterceptor> CreationInterceptors { get; }
    }
}