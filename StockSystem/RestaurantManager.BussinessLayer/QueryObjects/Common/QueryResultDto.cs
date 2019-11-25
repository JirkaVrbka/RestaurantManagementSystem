using System;
using System.Collections.Generic;
using RestaurantManager.BusinessLayer.DataTransferObjects;

namespace RestaurantManager.BusinessLayer.QueryObjects.Common
{
    public class QueryResultDto<TDto, TFilter> where TFilter : FilterDtoBase
    {
        /// <summary>
        /// Total number of items for the query
        /// </summary>
        public long TotalItemsCount { get; set; }

        /// <summary>
        /// Number of page (indexed from 1) which was requested
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The query results page
        /// </summary>
        public IEnumerable<TDto> Items { get; set; }

        /// <summary>
        /// Applied filter for this query
        /// </summary>
        public TFilter Filter { get; set; }

        public override string ToString()
        {
            return $"{TotalItemsCount} {typeof(TDto).Name}(s)" +
                   $"{(RequestedPageNumber != null ? $", page {RequestedPageNumber}/{Math.Ceiling(TotalItemsCount / (double)PageSize)}." : ".")}";
        }
    }
}
