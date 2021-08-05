using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Application.Common.DTO
{
    public class PaginationDTO<T> where T : class
    {
        public int PageNumber { get; }

        public int PageSize { get; }
        public int TotalPages { get; }
        public int TotalElements { get; }

        public IEnumerable<T> Items { get; }


        public PaginationDTO(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalElements = count;
            Items = items;
        }

        public bool HasPreviousPage => PageNumber > 1 && PageNumber <= TotalPages;

        public bool HasNextPage => PageNumber < TotalPages;

    }
}
