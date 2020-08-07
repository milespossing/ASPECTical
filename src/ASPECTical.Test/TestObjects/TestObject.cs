namespace ASPECTical.Test.TestObjects
{
    public interface ITestObject
    {
        void HasTestCounter();
        void NoTestCounter();
    }

    public class TestObject : ITestObject
    {
        [TestCounter]
        public virtual void HasTestCounter()
        {
            
        }

        public virtual void NoTestCounter()
        {
            
        }

        public void NotVirtual()
        {
            
        }
    }
}