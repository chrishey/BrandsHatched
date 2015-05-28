using System;
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
	    private readonly ILog _log;

	    public CircuitBreaker(ICircuitBreakerStore circuitBreakerStore, ILog log)
	    {
		    _circuitBreakerStore = circuitBreakerStore;
		    _log = log;
	    }

	    public bool IsOpen
	    {
		    get { return _circuitBreakerStore.CurrentState == CircuitBreakerState.Open; }
	    }

	    public bool IsClosed
	    {
		    get { return _circuitBreakerStore.CurrentState == CircuitBreakerState.Closed; }
	    }

	    public void ExecuteAction<T>(Task action)
	    {
		    var policy = Policy.Handle<Exception>().CircuitBreakerAsync(FailedCallThreshold, WaitTimeBeforeHalfOpen);

		    try
		    {
			    policy.ExecuteAsync(() => action);
		    }
		    catch (BrokenCircuitException exception)
		    {
			    _log.Log(exception.Message);
		    }
	    }

	    public int FailedCallThreshold
	    {
		    get { throw new NotImplementedException(); }
	    }

	    public TimeSpan WaitTimeBeforeHalfOpen
	    {
		    get { throw new NotImplementedException(); }
	    }
    }
}
