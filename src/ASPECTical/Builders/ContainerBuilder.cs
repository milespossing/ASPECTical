using ASPECTical.Injection;
using Castle.DynamicProxy;

namespace ASPECTical.Builders
{
    public class ContainerBuilder : IContainerBuilder
    {
        public IObjectSource ObjectSource { get; } = new ObjectSource();

        public IContainerBuilder WithCreationInterceptor(IObjectCreationInterceptor interceptor)
        {
            ObjectSource.CreationInterceptors.Add(interceptor);
            return this;
        }

        public IContainerBuilder WithInterceptor(IInterceptor interceptor)
        {
            ObjectSource.WithGlobalInterceptor(interceptor);
            return this;
        }

        public IContainerBuilder WithInterceptor(AttributedInterceptor interceptor)
        {
            ObjectSource.WithAttributedInterceptor(interceptor);
            return this;
        }
    }
}