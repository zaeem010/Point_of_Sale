using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class Supplier
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(455)]
        [Required(ErrorMessage = "Supplier Name Is Required")]
        public string Name { get; set; }
        [MaxLength(455)]
        [Required(ErrorMessage = "Company Name Is Required")]
        public string Company { get; set; }
        [MaxLength(455)]
        public string Address { get; set; }
        [MaxLength(455)]
        public string BanckName { get; set; }
        [MaxLength(455)]
        public string AccountNo { get; set; }
        [MaxLength(455)]
        public string AccountHolder { get; set; }
        [MaxLength(255)]
        public string NTN { get; set; }
        public double Gst { get; set; }
        public double SpDis { get; set; }
        public bool Verify { get; set; }
        public int ThirdLevelId { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
    }
}
