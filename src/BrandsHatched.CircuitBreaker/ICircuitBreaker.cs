using System;
using System.Threading.Tasks;

namespace BrandsHatched.CircuitBreaker
{
	public interface ICircuitBreaker
	{
		bool IsOpen { get; }
		bool IsClosed { get; }
		string State { get; }
		DateTime StateChanged { get; }
		void ExecuteAction(Action action);
		int FailedCallThreshold { get; }
		TimeSpan WaitTimeBeforeHalfOpen { get; }
	}
}