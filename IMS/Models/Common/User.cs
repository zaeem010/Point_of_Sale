using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedBy { get; set; }
        public string ImageUrl { get; set; }
        public string Contact { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsResetPasswordRequired { get; set; }

        #region Not Mapped Properties
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        [NotMapped]
        public IList<string> Roles { get; set; }
        [NotMapped]
        public bool IsAddedFromPortal { get; set; } = false;
        #endregion
    }
}
