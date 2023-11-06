namespace Shared.ApiDataTransferObject.Object
{
    public class PaginationBase : Pagination
    {
        public PaginationBase(Domain.ValueObjects.Meta meta)
        {
            if (meta != null)
            {
                this.CurrentPage = meta.CurrentPage;
                this.NextPage = meta.NextPage;
                this.PrevPage = meta.PrevPage;
                this.TotalItems = meta.TotalItems;
                this.PerPage = meta.PerPage;
                this.TotalPages = meta.TotalPages;
            }
        }
    }
}
