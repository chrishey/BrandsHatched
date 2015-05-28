using System;

namespace BrandsHatched.Web.ViewModel
{
	public class CircuitBreakerModel
	{
		public string State { get; set; }
		public DateTime LastStateChange { get; set; }
	}
}