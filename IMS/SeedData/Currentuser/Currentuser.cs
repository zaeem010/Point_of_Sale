using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace IMS.Currentuser
{
    public class Currentuser :ICurrentuser
    {
		private readonly IHttpContextAccessor _accessor;

		public Currentuser(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public string Username => _accessor.HttpContext?.User.Identity?.Name;

		public int GetUserId()
		{
			return IsAuthenticated() ? Convert.ToInt32(_accessor.HttpContext?.User.GetUserId()) : 0;
		}

		public string GetUserEmail()
		{
			return IsAuthenticated() ? _accessor.HttpContext?.User.GetUserEmail() : string.Empty;
		}

		public string GetFullName()
		{
			return IsAuthenticated() ? _accessor.HttpContext?.User.GetFullName() : string.Empty;
		}

		public bool IsAuthenticated()
		{
			return _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
		}

		public bool IsInRole(string role)
		{
			return _accessor.HttpContext?.User.IsInRole(role) ?? false;
		}

		public IEnumerable<Claim> GetUserClaims()
		{
			return _accessor.HttpContext?.User.Claims;
		}

		public HttpContext GetHttpContext()
		{
			return _accessor.HttpContext;
		}

		public string GetPhoneNumber()
		{
			return IsAuthenticated() ? _accessor.HttpContext?.User.GetPhoneNumber() : string.Empty;
		}
	}
}
