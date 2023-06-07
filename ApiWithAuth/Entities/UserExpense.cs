using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithAuth.Entities
{
    public class UserExpense
    {
        [Key]
        public int Id { get; set; }
        public string? Firstname { get; set; } 
        public string? Lastname { get; set; } 
        public string? Email { get; set; }
        public string? Username { get; set; } 
        public string? Password { get; set; }
        [NotMapped]
        public string? Token { get; set; } 
        public string? Role { get; set; } 
    }
}