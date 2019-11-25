namespace RestaurantManager.BusinessLayer.DataTransferObjects
{
    /// <summary>
    /// Base class for filtering queries
    /// </summary>
    public class FilterDtoBase
    {
        /// <summary>
        /// Number of page (indexed from 1) which was requested
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Name of the property for sorting query results
        /// </summary>
        public string SortCriteria { get; set; }

        /// <summary>
        /// Determines whether ASC sorting should be used
        /// </summary>
        public bool SortAscending { get; set; }
    }
}
