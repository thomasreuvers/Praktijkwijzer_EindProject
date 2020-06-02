using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaasVallei.Models
{
    public class RegisterUserModel
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Gebruikersnaam")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet teminste {2} karakters lang zijn.", MinimumLength = 6)]
        [DisplayName("Wachtwoord")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "De gegeven wachtwoorden komen niet overeen!")]
        [DisplayName("Bevestig Wachtwoord")]
        public string ConfirmPassword { get; set; }
    }
}
