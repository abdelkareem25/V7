namespace V7.Domain.Interfaces.Specifications
{
    public class ProductSpecParams
    {
        public string? sort { get; set; }
        public int? categoryId { get; set; }
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}

