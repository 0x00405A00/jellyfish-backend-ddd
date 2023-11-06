using Shared.DataFilter.Infrastructure;

namespace Infrastructure.Repository.Primitives
{
    public class RepositoryResponse<T>
    {
        public T Data { get; private set; }
        public Meta Meta { get; private set; }
        public ColumnSearchAggregateDTO ColumnSearchAggregateDTO { get; private set; }
        public RepositoryResponse(T data, ref Meta meta)
        {
            Data = data;
            Meta = meta;
        }
        public RepositoryResponse(T data, ref Meta meta, ColumnSearchAggregateDTO columnSearchAggregateDTO) : this(data,ref meta)
        {
            ColumnSearchAggregateDTO = columnSearchAggregateDTO;    
        }
    }
    public class Meta
    {
        public int TotalItems {  get; set; }
        public Meta() 
        {
        }
    }
}
