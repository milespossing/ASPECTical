using System;

namespace ASPECTical.Injection
{
    public class FuncObjectInterceptor : IObjectCreationInterceptor
    {
        private Func<object, object> _interceptor;

        public FuncObjectInterceptor(Func<object, object> interceptor)
        {
            _interceptor = interceptor;
        }

        public object InterceptCreatedObject(object value)
        {
            return _interceptor(value);
        }
    }
}