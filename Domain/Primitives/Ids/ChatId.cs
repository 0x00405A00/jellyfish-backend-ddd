namespace Domain.Primitives.Ids
{
    public record ChatId : Identification
    {
        public ChatId(Guid Uuid) : base(Uuid)
        {
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
