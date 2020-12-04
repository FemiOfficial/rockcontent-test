using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain.Models
{
    public class Posts
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Title_Tag { get; set; }
        public long Author { get; set; }
        public string Body { get; set; }
        public string Category { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }

    }
}
