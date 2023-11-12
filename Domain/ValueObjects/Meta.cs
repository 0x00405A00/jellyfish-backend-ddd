namespace Domain.ValueObjects
{
    public class Meta
    {
        public int TotalItems { get; private set; }
        public int PerPage { get; private set; } = -1;
        public int CurrentPage { get; private set; } = -1;
        public int TotalPages => CalculateTotalPages();
        public int NextPage => (TotalPages!= -1 ? (CurrentPage + 1> TotalPages?1:CurrentPage+1):-1); 
        public int PrevPage => (TotalPages!= -1 ? (CurrentPage - 1 < 1 ? TotalPages : CurrentPage - 1):-1);

        private Meta(int totalItems, int perPage, int currentPage) 
        { 
            TotalItems = totalItems;
            PerPage = perPage;
            CurrentPage = currentPage;
        }
        private int CalculateTotalPages()
        {
            int result = -1;
            if (PerPage != -1 && PerPage!=0 && TotalItems!=0)
            {
                result = (int)Math.Ceiling(((double)TotalItems / PerPage));
            }
            return result;
        }
        public static Meta Create(int totalItems, int perPage, int currentPage)
        {
            return new Meta(totalItems, perPage, currentPage);
        }
    }
}
