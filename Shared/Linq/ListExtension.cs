namespace Shared.Linq
{
    public static class ListExtension
    {
        public static bool IsGenericList(this object o)
        {
            if(o == null) return false;
            var oType = o.GetType();
            var genericTypeDef = oType.IsGenericType?oType.GetGenericTypeDefinition():null;
            bool isList = ((genericTypeDef!=null && (genericTypeDef == typeof(List<>) || genericTypeDef == typeof(ICollection<>))));
            return isList;
        }
    }
}
