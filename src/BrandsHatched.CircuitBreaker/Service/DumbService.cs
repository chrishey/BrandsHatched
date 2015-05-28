using System;

namespace BrandsHatched.CircuitBreaker.Service
{
	public class DumbService : IDumbService
	{
		public void DoSomething(bool successful)
		{
			if (successful)
				return;

			throw new DumbServiceException("Something went wrong!!!!!!!!!");
		}
	}

	public class DumbServiceException : Exception
	{
		public DumbServiceException(string message) : base(message)
		{}
	}
}
