namespace Domain.Primitives.Ids
{
    public record AuthId : Identification
    {
        public AuthId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
