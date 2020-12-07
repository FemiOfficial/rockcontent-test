using System;
namespace Api.Communication.Request
{
    public class QueryRequest
    {
        public int ViewPage { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 50;
    }
}
