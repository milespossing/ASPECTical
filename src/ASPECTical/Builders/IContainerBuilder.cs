using ASPECTical.Injection;
using Castle.DynamicProxy;

namespace ASPECTical
{
    public interface IContainerBuilder
    {
        IObjectSource ObjectSource { get; }
        IContainerBuilder WithCreationInterceptor(IObjectCreationInterceptor interceptor);
        IContainerBuilder WithInterceptor(IInterceptor interceptor);
        IContainerBuilder WithInterceptor(AttributedInterceptor interceptor);
    }
}