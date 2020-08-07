using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ASPECTical.Injection;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public class ObjectSource : IObjectSource
    {
        private IProxyGenerator _generator;
        public List<IInterceptor> GlobalInterceptors { get; } = new List<IInterceptor>();
        public List<IInterceptor> AttributedInterceptors { get; } = new List<IInterceptor>();
        public List<IObjectCreationInterceptor> CreationInterceptors { get; } = new List<IObjectCreationInterceptor>();

        public ObjectSource()
        {
            _generator = new ProxyGenerator();
        }

        public void WithGlobalInterceptor(IInterceptor interceptor)
        {
            GlobalInterceptors.Add(interceptor);
        }

        public void WithAttributedInterceptor(AttributedInterceptor interceptor)
        {
            AttributedInterceptors.Add(interceptor);
        }

        public T Create<T>(Type t = null)
        {
            var type = t ?? typeof(T);
            var applicableInterceptors = GetInterceptors(type);
            var proxy = _generator.CreateClassProxy(type, applicableInterceptors.ToArray());
            return (T) proxy;
        }

        private IEnumerable<IInterceptor> GetInterceptors(object o)
        {
            var type = o.GetType();
            return GetInterceptors(type);
        }

        private IEnumerable<IInterceptor> GetInterceptors(Type type)
        {
            var attributes = GetInterceptAttributes(type);
            var applicableInterceptors = GetInterceptors(attributes).ToList();
            applicableInterceptors.AddRange(GlobalInterceptors);
            return applicableInterceptors;
        }

        public T Create<T>(Func<T> creationMethod) where T : class
        {
            var inst = creationMethod.Invoke();
            return Create(inst);
        }

        public object CreateFromReal(object real)
        {
            var interceptors = GetInterceptors(real).ToArray();
            return CreateClass(real.GetType(),real,interceptors);
        }

        public T Create<T>(T real) where T : class
        {
            var interceptors = GetInterceptors(real).ToArray();
            if ((typeof(T).Attributes & TypeAttributes.Interface) != 0)
                return CreateInterface(real, interceptors);
            else
                return CreateClass(real, interceptors);
        }

        public T CreateInterface<T>(T real, IInterceptor[] interceptors) where T : class
        {
            var proxy = _generator.CreateInterfaceProxyWithTarget(real, ProxyGenerationOptions.Default,interceptors);
            return proxy;
        }

        public T CreateClass<T>(T real, IInterceptor[] interceptors) where T : class
        {
            var proxy = _generator.CreateClassProxyWithTarget(real, ProxyGenerationOptions.Default, interceptors);
            return proxy;
        }

        public object CreateClass(Type type, object real, IInterceptor[] interceptors)
        {
            var proxy = _generator.CreateClassProxyWithTarget(type, real, interceptors);
            return proxy;
        }

        public IEnumerable<InterceptAttribute> GetInterceptAttributes(Type t)
        {
            var attributes = t.GetMethods()
                .SelectMany(m => m.GetCustomAttributes(typeof(InterceptAttribute), true)).Cast<InterceptAttribute>();
            return attributes;
        }

        public IEnumerable<IInterceptor> GetInterceptors(IEnumerable<InterceptAttribute> attributes)
        {
            var interceptors = attributes.Select(a =>
                    AttributedInterceptors.FirstOrDefault(interceptor => interceptor.GetType() == a.InterceptorType))
                .Where(i => i != null);
            return interceptors;
        }
    }
}