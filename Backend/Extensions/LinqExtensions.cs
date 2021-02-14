using MentorApp.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MentorApp.Extensions
{
    public static class LinqExtensions
    {
        private const int DefaultPageSize = 10;

        public static IQueryable<T> GetPage<T>(this IQueryable<T> rows, int pageNumber = 1, int pageSize = DefaultPageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Page number must be greater than 1");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size cannot be less than zero");
            }

            return rows.Take(pageSize).Skip(pageNumber - 1);
        }

        public static PagedListItemDTO<T> ToPagedList<T>(this IEnumerable<T> rows, int pageNumber = 1, int pageSize = DefaultPageSize, int pagesTotal = 1)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentException("Page number must be greater than 1");
            }

            if (pageSize < 1)
            {
                throw new ArgumentException("Page size cannot be less than zero");
            }

            if (pagesTotal < 1)
            {
                throw new ArgumentException("Number of pages must be greated than zero");
            }

            return new PagedListItemDTO<T>
            {
                Data = rows,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                ItemsTotal = pagesTotal
            };
        }
    }
}
