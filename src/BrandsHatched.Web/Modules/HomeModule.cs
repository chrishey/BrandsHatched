using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using BrandsHatched.Web.ViewModel;
using Nancy;

namespace BrandsHatched.Web.Modules
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ => GetCurrentState();
		}

		private dynamic GetCurrentState()
		{
			var model = new CircuitBreakerModel();
			return View["Home", model];
		}
	}
}