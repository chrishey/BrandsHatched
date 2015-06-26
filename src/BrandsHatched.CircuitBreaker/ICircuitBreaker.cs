using System;

namespace BrandsHatched.CircuitBreaker
{
	public interface ICircuitBreaker
	{
		void Configure(Type exceptionToHandle, int failureThreshold, int waitTimeInMinutes);
		bool IsOpen { get; }
		bool IsClosed { get; }
		string State { get; }
		DateTime StateChanged { get; }
		void ExecuteAction(Action action, string key);
		int FailedCallThreshold { get; }
		TimeSpan WaitTimeBeforeHalfOpen { get; }
	}
}