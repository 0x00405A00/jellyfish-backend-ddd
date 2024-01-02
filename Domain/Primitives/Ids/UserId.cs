namespace Domain.Primitives.Ids
{
    public record UserId : Identification
    {
        public UserId(Guid guid) : base(guid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
