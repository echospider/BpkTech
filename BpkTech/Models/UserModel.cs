using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BpkTech.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage ="Maximum Length is 50")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50")]
        public string City { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length is 20")]
        public string PhoneNumber { get; set; }
    }
}
