using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationDotNetCore.Helper
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items,int count, int pageIndex, int totalPages)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling( Count / (double) totalPages);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int totalPages)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * totalPages).Take(totalPages).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, totalPages);
        }
    }
}
