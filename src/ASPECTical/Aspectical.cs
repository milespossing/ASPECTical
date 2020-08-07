using ASPECTical.Builders;

namespace ASPECTical
{
    public class Aspectical
    {
        public static IContainerBuilder BuildContainer()
        {
            return new ContainerBuilder();
        }

        public static IObjectSource BuildObjectSource()
        {
            return new ObjectSource();
        }
    }
}