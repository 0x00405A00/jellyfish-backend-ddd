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
        public static ICollection<TIdentification> ToIdentification<TIdentification>(this ICollection<Guid> guid)
            where TIdentification : Identification
        {
            ICollection<TIdentification> response = new List<TIdentification>();
            foreach (var g in guid)
            {

                var identification = (TIdentification)Activator.CreateInstance(typeof(TIdentification), new object[] { g })!;
                response.Add(identification);
            }
            return response;
        }
        public static Guid ToGuid(this Identification identification)
        {
            return identification.Id;
        }
        public static bool IsValid(this Identification identification)
        {
            return identification.Id != Guid.Empty;
        }
    }
}
