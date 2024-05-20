using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace POC_TDD_App.Model
{
    public class CustomerModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name must not be empty.")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage ="Please enter correct email id.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone must not be empty.")]
        public string Phone { get; set; } 
        public string City { get; set; }
    }
}
