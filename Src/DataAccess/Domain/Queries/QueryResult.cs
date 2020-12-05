using System;
using System.Collections.Generic;

namespace DataAccess.Domain.Queries
{
    public class QueryResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalItems { get; set; } = 0;
        public int ViewPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 50;
    }
}
