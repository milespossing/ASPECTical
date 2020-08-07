using System;

namespace ASPECTical.Test.TestObjects
{
    public class TestCounterAttribute : InterceptAttribute
    {
        public TestCounterAttribute() : base(typeof(TestInterceptor))
        {
        }
    }
}