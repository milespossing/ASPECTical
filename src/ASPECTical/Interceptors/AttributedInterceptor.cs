using System;
using System.Linq;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public abstract class AttributedInterceptor : IInterceptor
    {
        public abstract void Intercept(IInvocation invocation);
    }
    
    public abstract class AttributedInterceptor<TAttribute> : AttributedInterceptor where TAttribute : Attribute
    {
        public override void Intercept(IInvocation invocation)
        {
            if (!(invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TAttribute), true)
                .FirstOrDefault() is TAttribute attribute))
                invocation.Proceed();
            else
                Intercept(invocation, attribute);
        }

        public abstract void Intercept(IInvocation invocation, TAttribute attribute);
    }
}