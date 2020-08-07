using System;

namespace ASPECTical
{
    public class InterceptAttribute : Attribute
    {
        public Type InterceptorType { get; }

        public InterceptAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }
}