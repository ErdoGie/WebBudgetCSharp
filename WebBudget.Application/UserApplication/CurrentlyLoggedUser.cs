using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBudget.Application.UserApplication
{
	public class CurrentlyLoggedUser
	{
		public string Id { get; set; }


		public CurrentlyLoggedUser(string id)
		{
			Id = id;

		}

	}
}
