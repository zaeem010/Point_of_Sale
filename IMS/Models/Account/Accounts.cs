using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class AccountHead
    {
        [Key]
        public int Id { get; set; }
        public string Head_Name { get; set; }
    }
    public class Firstlevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int AccountHeadId { get; set; }
        [MaxLength(455)]
        public string AccountTitle { get; set; }
        public virtual AccountHead AccountHead { get; set; }
    }
    public class Secondlevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int AccountHeadId { get; set; }
        public int FirstlevelId { get; set; }
        [MaxLength(455)]
        public string AccountTitle { get; set; }
        public virtual AccountHead AccountHead { get; set; }
        public virtual Firstlevel Firstlevel { get; set; }
    }
    public class ThirdLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int AccountHeadId { get; set; }
        public int FirstlevelId { get; set; }
        public int SecondlevelId { get; set; }
        [MaxLength(455)]
        public string AccountTitle { get; set; }
        [MaxLength(455)]
        public string AccountType { get; set; }
        public double Dr { get; set; }
        public double Cr { get; set; }

        public virtual AccountHead AccountHead { get; set; }
        public virtual Firstlevel Firstlevel { get; set; }
        public virtual Secondlevel Secondlevel { get; set; }
    }

    public class TranscationDetails
    {
        [Key]
        public long Id { get; set; }
        public long Invid { get; set; }
        [MaxLength(155)]
        public string Vtype { get; set; }
        [MaxLength(455)]
        public string TransDes { get; set; }
        public DateTime TransDate { get; set; }
        public int ThirdLevelId { get; set; }
        public double Dr { get; set; }
        public double Cr { get; set; }

        public int UserId { get; set; }
        public int HeadId { get; set; }
        public virtual ThirdLevel ThirdLevel { get; set; }
        public virtual User User { get; set; }
    }
}
