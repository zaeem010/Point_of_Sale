using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class Customer
    {
        public long Id { get; set; }
        [MaxLength(455)]
        [Required (ErrorMessage ="Customer Name Is Required")]
        public string Name { get; set; }
        [MaxLength(455)]
        public string FatherName { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage ="Customer cell No Is Required")]
        public string ContactNo { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage ="Customer CNIC Is Required")]
        public string Cnic { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage ="Customer Cheque No Is Required")]
        public string ChequeNo { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage ="Customer Address Is Required")]
        public string Address { get; set; }
        public int ThirdLevelId { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
        public string ImageUrl { get; set; }
        public string CnicFront { get; set; }
        public string CnicBack { get; set; }
        public bool Verify { get; set; }
        public List<CustomerGurantor> CustomerGurantorList { get; set; }
        public Customer()
        {
            CustomerGurantorList = new List<CustomerGurantor>();
        }
    }
    public class CustomerGurantor
    {
        [Key]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public long GurantorId { get; set; }
        public virtual Gurantor Gurantor { get; set; }
    }
    public class Gurantor
    {
        public long Id { get; set; }
        [MaxLength(455)]
        [Required (ErrorMessage = "Gurantor Name Is Required")]
        public string Name { get; set; }
        [MaxLength(455)]
        public string FatherName { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage = "Gurantor cell No Is Required")]
        public string ContactNo { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage = "Gurantor CNIC Is Required")]
        public string Cnic { get; set; }
        [MaxLength(155)]
        [Required (ErrorMessage = "Gurantor Cheque No Is Required")]
        public string ChequeNo { get; set; }

        public string ImageUrl { get; set; }
        public string CnicFront { get; set; }
        public string CnicBack { get; set; }
        public bool Verify { get; set; }
    }
}
