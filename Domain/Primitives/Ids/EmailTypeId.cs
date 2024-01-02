namespace Domain.Primitives.Ids
{
    public record EmailTypeId : Identification
    {
        public EmailTypeId(Guid Uuid) : base(Uuid)
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
