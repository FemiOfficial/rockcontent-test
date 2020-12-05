using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain.Models
{
    public class Likes
    {
        [Key]
        public long Id { get; set; }
        //public long LikeId { get; set; }

        public string RequestIpAddress { get; set; }
        public string RequestUserAgent { get; set; }
        public string PostId { get; set; }
        public string ClientReferenceId { get; set; }
        public string RequestUsername { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
