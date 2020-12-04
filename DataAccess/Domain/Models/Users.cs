using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Domain.Models
{
    public class Users
    {
        [Key]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public DateTime createdAt {get; set; }
        public DateTime updatedAt { get; set; }
    }
}
    