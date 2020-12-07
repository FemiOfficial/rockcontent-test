using System;
using System.Collections.Generic;
using Api.Helpers;
namespace Api.Communication.Response
{
    public class LikeResponseDto
    {
        public string RequestIpAddress { get; set; }
        public string RequestUserAgent { get; set; }
        public string PostId { get; set; }
        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }
    }

    public class LikesQueryResult<T>
    {
        public int TotalLikes { get; set; } = 0;
        public List<T> Likes { get; set; } = new List<T>();
        public int ViewPage { get; set; } = 1;
        public int LikesPerPage { get; set; } = 50;
    }
}
