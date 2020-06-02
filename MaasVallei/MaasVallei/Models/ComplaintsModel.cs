using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MaasVallei.Models
{
    public class ComplaintsModel
    {
        public string Id { get; set; }

        [DisplayName("Email")]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Titel")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Omschrijving")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Categorie")]
        public Departments Department { get; set; }

        [Required]
        [DisplayName("Prioriteit")]
        public Priority Priority { get; set; }

        public DateTime DateOfCreation { get; set; }

        public string State { get; set; }


        public string UserId { get; set; }
        public string FormOption { get; set; }
    }

    public enum Departments
    {
        Algemeen,
        Technisch,
        Schoonmaak
    }

    public enum Priority
    {
        Hoog = 3,
        Middel = 2,
        Laag = 1
    }
}
