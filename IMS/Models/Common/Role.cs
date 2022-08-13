using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
	public class Role : IdentityRole<int>
	{
		public string Description { get; set; }
		public virtual ICollection<RoleClaim> RoleClaims { get; set; }

		public Role() : base()
		{
			RoleClaims = new HashSet<RoleClaim>();
		}

		public Role(string roleName, string roleDescription = null) : base(roleName)
		{
			RoleClaims = new HashSet<RoleClaim>();
			Description = roleDescription;
		}
	}
	public class RoleClaimsModel
	{
		public int RoleId { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
		public bool Selected { get; set; }
	}
	public class PermissionModel
	{
		public int RoleId { get; set; }
		public IList<RoleClaimsModel> RoleClaims { get; set; }
	}
}
