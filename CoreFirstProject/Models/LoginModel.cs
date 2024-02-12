using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoreFirstProject.Models
{
    public class LoginModel
    {
        public string Name { get; set; }
        [Key]
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
