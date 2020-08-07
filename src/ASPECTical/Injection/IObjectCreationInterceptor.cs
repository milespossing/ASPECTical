namespace ASPECTical.Injection
{
    public interface IObjectCreationInterceptor
    {
        object InterceptCreatedObject(object value);
    }
}