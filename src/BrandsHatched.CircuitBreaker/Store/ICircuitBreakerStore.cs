using System;

namespace BrandsHatched.CircuitBreaker.Store
{
	public interface ICircuitBreakerStore
	{
		CircuitBreakerState CurrentState { get; }
		DateTime StateLastChanged { get; }
		void Trip(Exception exception);
		void Reset();

		IBrokenCircuits BrokenCircuits { get; }
	}
}