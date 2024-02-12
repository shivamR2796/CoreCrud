using System.ComponentModel.DataAnnotations;

namespace CoreFirstProject.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public  Double Amount { get; set; }
    }
}
