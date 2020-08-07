using System;
using ASPECTical.Injection;
using ASPECTical.Test.TestObjects;
using NUnit.Framework;

namespace ASPECTical.Test
{
    [TestFixture]
    public class BuildersTests
    {
        [Test]
        public void CanGetBuilderFromAspectical()
        {
            var builder = Aspectical.BuildContainer();
            Assert.NotNull(builder);
        }

        [Test]
        public void CanAddInterceptor()
        {
            Aspectical.BuildContainer()
                .WithInterceptor(new BeforeInvokeInterceptor(() => Console.WriteLine("Intercepted")));
        }

        [Test]
        public void CanAddAttributedInterceptor()
        {
            Aspectical.BuildContainer()
                .WithInterceptor(new TestInterceptor());
        }

        [Test]
        public void CanAddCreationInterceptor()
        {
            Aspectical.BuildContainer()
                .WithCreationInterceptor(new FuncObjectInterceptor(o =>
                {
                    Console.WriteLine(o);
                    return o;
                }));
        }
    }
}