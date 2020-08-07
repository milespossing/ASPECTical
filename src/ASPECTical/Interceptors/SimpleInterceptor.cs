using System;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public class SimpleInterceptor : IInterceptor
    {
        private Action<IInvocation> _action;

        public SimpleInterceptor(Action<IInvocation> action)
        {
            _action = action;
        }

        public void Intercept(IInvocation invocation)
        {
            _action(invocation);
        }
    }
}