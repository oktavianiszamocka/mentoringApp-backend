using System.Collections.Generic;

namespace MentorApp.DTOs.Responses
{
    public class PagedListItemDTO<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int ItemsTotal { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
