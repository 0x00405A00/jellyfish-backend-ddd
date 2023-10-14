namespace Presentation.Swagger.Attribute
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OpenApiIgnoreMethodParameter : System.Attribute
    {
    }
}
