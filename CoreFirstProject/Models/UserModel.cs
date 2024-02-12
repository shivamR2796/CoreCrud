using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreFirstProject.Models
{
    public class UserModel
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
