using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Resources.Request.LikesRequest
{
    public class LikeRequestDto
    {
        // Will be captured from the HTTP Request
        public string RequestIpAddress { get; set; }

        // Will be captured from the HTTP Request
        public string RequestUserAgent { get; set; }

        [Required]
        public string PostId { get; set; }

        [Required]
        public string ClientReferenceId { get; set; }

        [Required]
        public string RequestUsername { get; set; }
    }


    public class DisLikeRequestDto
    {
        public long LikeId { get; set; }
        public string RequestIpAddress { get; set; }
        public string RequestUserAgent { get; set; }
        public long PostId { get; set; }
        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }
    }

    public class LikesQueryRequestDto : QueryRequest
    {
        [Required]
        public string PostId { get; set; }

        public string RequestIpAddress { get; set; }
        public string RequestUserAgent { get; set; }
        

        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }

    }





    
}
