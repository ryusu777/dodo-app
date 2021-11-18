namespace DodoApp.Contracts.V1.Responses
{
    public class PageWrapper<T>
    {
        public T Data { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalPage { get; set; }
        public int? ItemPerPage { get; set; }
        public int? RowsNumber { get; set; }
        public string SortBy { get; set; }
        public string Descending { get; set; }
        public string SearchText { get; set; }
    }
}