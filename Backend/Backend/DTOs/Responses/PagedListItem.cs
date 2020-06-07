using System.Collections.Generic;

namespace Backend.DTOs.Responses
{
    public class PagedListItem<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int ItemsTotal { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
