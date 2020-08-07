using Castle.DynamicProxy;

namespace ASPECTical.Test.TestObjects
{
    public class TestInterceptor : AttributedInterceptor<TestCounterAttribute>
    {
        public TestInterceptor(int increment = 1)
        {
            Increment = increment;
        }

        public int Count { get; set; }
        public int Increment { get; }
        
        public override void Intercept(IInvocation invocation, TestCounterAttribute attribute)
        {
            Count++;
            invocation.Proceed();
        }
    }
}