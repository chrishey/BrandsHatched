using System;
using BrandsHatched.CircuitBreaker.Logging;

namespace BrandsHatched.CircuitBreaker.Store
{
	public interface ICircuitBreakerStore
	{
		ILog Logger { get; }
		CircuitBreakerState CurrentState { get; }
		DateTime StateLastChanged { get; }
		void Trip(Exception exception);
		void Reset();
	}
}