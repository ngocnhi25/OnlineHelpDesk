namespace Application.DTOs
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage => Page * Limit < TotalCount;
        public bool HasPreviousPage => Page > 1;
    }
}
