using Domain.Primitives;

namespace Domain.Extension
{
    public static class IdentificationExtension
    {
        public static TIdentification ToIdentification<TIdentification>(this Guid guid)
            where TIdentification : Identification
        {

            var identification = (TIdentification)Activator.CreateInstance(typeof(TIdentification), new object[] { guid })!;

            return identification;
        }
        /*public static Guid ToGuid(this Identification identification)
        {
            return identification.Id;
        }*/
        public static bool IsValid(this Identification identification)
        {
            return identification.Id != Guid.Empty;
        }
    }
}
