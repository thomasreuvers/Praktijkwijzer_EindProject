using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Services;

namespace MaasVallei.Models
{
    public class ReservationModel
    {
        [AllowNull]
        public string ReservationId { get; set; }

        [AllowNull]
        public string FormOption { get; set; }

        [Required]
        [DisplayName("Telefoon")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Email")]
        public string EmailAddress { get; set; }
        [Required]
        [DisplayName("Aankomst")]
        public DateTime ArriveDate { get; set; }
        [Required]
        [DisplayName("Vertrek")]
        public DateTime DepartureDate { get; set; }

        public string UserId { get; set; }

    }
}
