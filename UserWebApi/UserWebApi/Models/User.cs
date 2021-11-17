using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserWebApi.CustomValidations;

namespace UserWebApi.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [CustomEmptyCheck]
        public string Name { get; set; }

        [CustomEmptyCheck]
        public string Designation { get; set; }

        public DateTime JoiningDate { get; set; }

        public string FullAddress { get; set; }

        [CustomEmptyCheck]
        public string Country { get; set; }

        public string ImagePath { get; set; }

    }
}
