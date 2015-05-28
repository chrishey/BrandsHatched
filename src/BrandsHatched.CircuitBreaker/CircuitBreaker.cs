using System;
using System.Configuration;
using System.Threading.Tasks;
using BrandsHatched.CircuitBreaker.Logging;
using BrandsHatched.CircuitBreaker.Store;
using Polly;
using Polly.CircuitBreaker;

namespace BrandsHatched.CircuitBreaker
{
    public class CircuitBreaker : ICircuitBreaker
    {
	    private readonly ICircuitBreakerStore _circuitBreakerStore;

	    public CircuitBreaker(ICircuitBreakerStore circuitBreakerStore)
	    {
		    _circuitBreakerStore = circuitBreakerStore;
	    }

	    public bool IsOpen
	    {
		    get { return _circuitBreakerStore.CurrentState == CircuitBreakerState.Open; }
	    }

	    public bool IsClosed
	    {
		    get { return _circuitBreakerStore.CurrentState == CircuitBreakerState.Closed; }
	    }

	    public async void ExecuteAction(Func<Task> action)
	    {
		    var policy = Policy.Handle<Exception>().CircuitBreakerAsync(FailedCallThreshold, WaitTimeBeforeHalfOpen);

		    try
		    {
			    await policy.ExecuteAsync(action);
		    }
		    catch (BrokenCircuitException exception)
		    {
			    _circuitBreakerStore.Trip(exception);
		    }
	    }

	    public int FailedCallThreshold
	    {
		    get { return Convert.ToInt32(ConfigurationManager.AppSettings["AllowedFailedCalls"]); }
	    }

	    public TimeSpan WaitTimeBeforeHalfOpen
	    {
		    get { return TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["WaitTimeForHalfOpen"])); }
	    }
    }
}
