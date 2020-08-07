using ASPECTical.Test.TestObjects;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace ASPECTical.Test
{
    [TestFixture]
    public class ObjectSourceTests
    {
        [Test]
        public void CanGetFromAspectical()
        {
            var source = Aspectical.BuildObjectSource();
            Assert.NotNull(source);
        }
        
        [Test]
        public void CanCreateSimpleProxies()
        {
            var objectSource = new ObjectSource();
            var attributedInterceptor = new TestInterceptor();
            objectSource.WithAttributedInterceptor(attributedInterceptor);
            var testObject = objectSource.Create<TestObject>();
            Assert.AreEqual(0, attributedInterceptor.Count);
            testObject.HasTestCounter();
            Assert.AreEqual(1, attributedInterceptor.Count);
            testObject.HasTestCounter();
            Assert.AreEqual(2, attributedInterceptor.Count);
            testObject.NoTestCounter();
            Assert.AreEqual(2, attributedInterceptor.Count);
        }

        [Test]
        public void CanCreateProxyFromTarget()
        {
            var objectSource = new ObjectSource();
            var testObject = objectSource.Create(new TestObject());
            Assert.NotNull(testObject);
        }

        [Test]
        public void CanCreateProxyInterfaceFromTarget()
        {
            var objectSource = new ObjectSource();
            var testObject = objectSource.Create<ITestObject>(new TestObject());
            Assert.NotNull(testObject);
        }

        [Test]
        public void CanAddGlobalInterceptor()
        {
            int counter = 0;
            var interceptor = new SimpleInterceptor(a => { counter++; a.Proceed();});
            var objectSource = new ObjectSource();
            objectSource.WithGlobalInterceptor(interceptor);
            var testObject = objectSource.Create<TestObject>();
            Assert.AreEqual(0, counter);
            testObject.HasTestCounter();
            Assert.AreEqual(1, counter);
            testObject.NoTestCounter();
            Assert.AreEqual(2, counter);
            testObject.NotVirtual();
            Assert.AreEqual(2, counter);
        }

        [Test]
        public void CanCreateByFunc()
        {
            var objectSource = new ObjectSource();
            int counter = 0;
            objectSource.WithGlobalInterceptor(new BeforeInvokeInterceptor(() => counter++));
            var testObject = objectSource.Create(() => new TestObject());
            testObject.NoTestCounter();
            Assert.AreEqual(1, counter);
        }

        [Test]
        public void CanCreateObjectFromReal()
        {
            var objectSource = new ObjectSource();
            int counter = 0;
            objectSource.WithGlobalInterceptor(new BeforeInvokeInterceptor(() => counter++));
            var testObject = (TestObject) objectSource.CreateFromReal(new TestObject());
            Assert.NotNull(testObject);
            testObject.HasTestCounter();
            Assert.AreEqual(1, counter);
        }
    }
}