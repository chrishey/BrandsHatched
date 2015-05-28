using System;
using System.Threading.Tasks;

namespace BrandsHatched.CircuitBreaker
{
	public interface ICircuitBreaker
	{
		bool IsOpen { get; }
		bool IsClosed { get; }
		void ExecuteAction<T>(Task action);
		int FailedCallThreshold { get; }
		TimeSpan WaitTimeBeforeHalfOpen { get; }
	}
}