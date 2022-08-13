using System;
using System.Security.Claims;

namespace IMS.Currentuser
{
    public static class ClaimsPrincipalExtensions
	{
		public static int GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
			return Convert.ToInt32(claim?.Value);
		}

		public static string GetUserEmail(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			var claim = principal.FindFirst(ClaimTypes.Email);
			return claim?.Value;
		}

		public static string GetFullName(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			var claim = principal.FindFirst("FullName");
			return claim?.Value;
		}

		public static string GetPhoneNumber(this ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			var claim = principal.FindFirst(ClaimTypes.MobilePhone);
			return claim?.Value;
		}
	}
}
