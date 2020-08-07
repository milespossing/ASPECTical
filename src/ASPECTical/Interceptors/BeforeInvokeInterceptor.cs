using System;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public class BeforeInvokeInterceptor : IInterceptor
    {
        private Action _action;

        public BeforeInvokeInterceptor(Action action)
        {
            _action = action;
        }

        public void Intercept(IInvocation invocation)
        {
            _action.Invoke();
            invocation.Proceed();
        }
    }
}