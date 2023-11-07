namespace Shared.Linq
{
    public static class ListExtension
    {
        public static bool IsGenericList(this object o)
        {
            if(o == null) return false;
            var oType = o.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }
    }
}
