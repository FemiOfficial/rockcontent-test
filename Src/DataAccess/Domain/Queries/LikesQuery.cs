using System;


namespace DataAccess.Domain.Queries
{
    public class LikesQuery : Query
    {
        public string RequestIpAddress { get; set; }
        public string RequestUserAgent { get; set; }
        public string PostId { get; set; }
    
        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }

        public LikesQuery(string IpAddress = null, string userAgent = null, string clientReferenceId = null, string requestUsername = null,
            string postId = null, int page = 1, int itemsPerPage = 50) : base(page, itemsPerPage)
        {
            RequestIpAddress = IpAddress;
            RequestUserAgent = userAgent;
            ClientReferenceId = clientReferenceId;
            RequestUsername = requestUsername;
            PostId = postId;
        }
    }
}
