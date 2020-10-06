using System;

namespace SharpApi
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiEndpointAttribute : Attribute
    {
        public string Route { get; set; }
        public string Method { get; set; }
        public bool UseGetForHead { get; set; }

        public ApiEndpointAttribute(string name, string method = "GET", bool useGetForHead = false)
        {
            Route = name;
            Method = method.ToUpper();
            UseGetForHead = useGetForHead;
        }
    }
}
