using System;
using System.ComponentModel.DataAnnotations;


namespace Api.Communication.Request
{

    public class LikeDto
    {
        // Will be captured from the HTTP Request
        public string RequestIpAddress { get; set; }

        // Will be captured from the HTTP Request
        public string RequestUserAgent { get; set; }

        public string PostId { get; set; }

        public string ClientReferenceId { get; set; }

        public string RequestUsername { get; set; }


    }

    public class LikeRequestDto
    {

        [Required]
        public string PostId { get; set; }

        [Required]
        public string ClientReferenceId { get; set; }

        [Required]
        public string RequestUsername { get; set; }


    }


    public class DisLikeRequestDto
    { 
        public string PostId { get; set; }
        [Required]
        public string ClientReferenceId { get; set; }
        [Required]
        public string RequestUsername { get; set; }
    }

    public class LikesQueryByPostRequestDto : QueryRequest
    {
        [Required]
        public string PostId { get; set; }      

        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }

    }

    public class LikesQueryByClientReferenceIdRequestDto : QueryRequest
    {
        [Required]
        public string ClientReferenceId { get; set; }
        public string PostId { get; set; }

        public string RequestUsername { get; set; }

    }






}
