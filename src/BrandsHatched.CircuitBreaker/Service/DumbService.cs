using System;

namespace BrandsHatched.CircuitBreaker.Service
{
	public class DumbService : IDumbService
	{
		public void DoSomething(bool successful)
		{
			if (successful)
				return;

			throw new Exception("Something went wrong!!!");
		}
	}
}
