using MentorApp.Filter;
using System;

namespace MentorApp.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
