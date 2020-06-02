using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MaasVallei.Models
{
    public class UserModel
    {
        [Required]
        public string Username { get; set; }
        [AllowNull]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
