using System;
using System.Linq;

namespace Backend.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> rows, int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than 1");
            if (pageSize < 1) throw new ArgumentException("Page size cannot be less than zero");

            return rows.Take(pageSize).Skip(pageNumber - 1);
        }
    }
}
