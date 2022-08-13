using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;

namespace IMS.Currentuser
{
    public interface ICurrentuser
    {
		string Username { get; }

		int GetUserId();

		string GetUserEmail();

		string GetPhoneNumber();

		string GetFullName();

		bool IsAuthenticated();

		bool IsInRole(string role);

		IEnumerable<Claim> GetUserClaims();

		HttpContext GetHttpContext();
	}
}
