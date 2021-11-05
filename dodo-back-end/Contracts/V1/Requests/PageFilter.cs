namespace DodoApp.Contracts.V1.Requests
{
    public class PageFilter
    {
        private readonly int _maxItemPerPage = 100;
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
        public string SortBy { get; set; }
        public string Descending { get; set; }
        public string SearchText { get; set; }

        public PageFilter()
        {
            this.Page = 1;
            this.RowsPerPage = _maxItemPerPage;
            this.SortBy = null;
            this.Descending = null;
            this.SearchText = null;
        }

        public PageFilter(int page, int itemPerPage)
        {
            this.Page = page < 1 ? 1 : page;
            this.RowsPerPage = itemPerPage > _maxItemPerPage ? _maxItemPerPage : itemPerPage;
        }

        public PageFilter(int page, int itemPerPage, string sortBy, string sortDesc, string searchText)
        {
            this.Page = page < 1 ? 1 : page;
            this.RowsPerPage = itemPerPage > _maxItemPerPage ? _maxItemPerPage : itemPerPage;
            this.SortBy = sortBy;
            this.Descending = sortDesc;
            this.SearchText = searchText;
        }
    }
}
