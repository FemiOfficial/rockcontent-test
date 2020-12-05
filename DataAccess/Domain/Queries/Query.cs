using System;
namespace DataAccess.Domain.Queries
{
    public class Query
    {
        public int ViewPage { get; protected set; }
        public int ItemsPerPage { get; protected set; }
   

        public Query(int viewPage, int itemsPerPage)
        {
            ViewPage = viewPage;
            ItemsPerPage = itemsPerPage;
     

            if (ViewPage <= 0)
            {
                ViewPage = 1;
            }

            if (ItemsPerPage <= 0 || ItemsPerPage > 50)
            {
                ItemsPerPage = 50;
            }
        }
    }
}
