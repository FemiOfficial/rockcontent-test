using System;
namespace Api.Tests.Resources
{
    public static class ApiRoutes
    {
        private static readonly string _baseUrl = "http://localhost:13151/api/likefeature/";

        public static class Likes
        {
            public static readonly string _likePosturl = string.Concat(_baseUrl, "likepost");

            public static readonly string _dislikePosturl = string.Concat(_baseUrl, "/dislikepost/{postId}");

            public static readonly string LikesByPost = string.Concat(_baseUrl, "/likesbypost");

            public static readonly string LikesByClientReferenceId = string.Concat(_baseUrl, "/likesbyclientreferenceid");

        }
    }
}
