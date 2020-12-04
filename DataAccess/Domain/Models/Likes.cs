using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain.Models
{
    public class Likes
    {
        [Key]
        public long Id { get; set; }
        public long Post_Id { get; set; }
        public long User_Id { get; set; }

    }
}
