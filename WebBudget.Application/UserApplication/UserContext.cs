using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.UserApplication
{
	public interface IUserContext
	{
		CurrentlyLoggedUser GetCurrentlyLoggedUser();
	}

	public class UserContext : IUserContext
	{
		//wykorzystuje kontener zaleznosci aby wydobyc informacje o zapytaniu HTTP
		// wstrzykuje obiekt IHTTPAcesssor ktory pozwala uzyskac dostep do kontekstu 
		//HTTP

		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserContext(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public CurrentlyLoggedUser GetCurrentlyLoggedUser()
		{

			//w zaleznosci od tego czy uzyszkodnik jest zalogowany czy nie,
			// loggedUser będzie nullem lub też nie.
			var loggedUser = _httpContextAccessor?.HttpContext?.User;

			if (loggedUser == null)
			{
				throw new InvalidOperationException("User Context not available");
			}

			var userId = loggedUser.FindFirst(u => u.Type == ClaimTypes.NameIdentifier)!.Value;

			return new CurrentlyLoggedUser(userId);

		}
	}
}
