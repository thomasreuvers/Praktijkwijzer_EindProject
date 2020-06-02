using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MaasVallei.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MaasVallei.Models
{
    public class RoosterModel
    {
        [AllowNull]
        public string Id { get; set; }
        [AllowNull]
        public User User { get; set; }
        [AllowNull]
        public List<SelectListItem> AllUsers { get; set; }

        [Required]
        public string SelectedUser { get; set; }
        [Required]
        public DateTime StartShift { get; set; }
        [Required]
        public DateTime EndShift { get; set; }
        [Required]
        public DateTime RoosterDate { get; set; }

    }
}
