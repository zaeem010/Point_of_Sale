using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Models
{
    public class ModelNo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
    }
}
