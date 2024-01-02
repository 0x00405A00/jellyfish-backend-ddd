namespace Domain.Primitives.Ids
{
    public record UserTypeId : Identification
    {
        public UserTypeId(Guid guid) : base(guid)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
