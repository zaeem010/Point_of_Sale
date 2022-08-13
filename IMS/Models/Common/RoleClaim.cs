using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class RoleClaim : IdentityRoleClaim<int>
	{
		public string Description { get; set; }
		public string Group { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        //public RoleClaim(string roleClaimDescription = null, string roleClaimGroup = null) : base()
        //{
        //    Description = roleClaimDescription;
        //    Group = roleClaimGroup;
        //}

        [NotMapped]
        public bool Selected { get; set; }
    }
}
