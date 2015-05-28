using System;
using System.Configuration;
using System.Threading.Tasks;
using BrandsHatched.CircuitBreaker.Logging;
using BrandsHatched.CircuitBreaker.Service;
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

	    public string State
	    {
		    get { return _circuitBreakerStore.CurrentState.ToString(); }
	    }

	    public DateTime StateChanged
	    {
		    get { return _circuitBreakerStore.StateLastChanged; }
	    }

	    public void ExecuteAction(Action action)
	    {
			var policy = Policy.Handle<DumbServiceException>().CircuitBreaker(FailedCallThreshold, WaitTimeBeforeHalfOpen);

		    try
		    {
			    policy.Execute(action);
				if(IsOpen)
					_circuitBreakerStore.Reset();
		    }
		    catch (BrokenCircuitException exception)
		    {
			    _circuitBreakerStore.Trip(exception);
		    }
		    catch (DumbServiceException ex)
		    {
			    _log.Log(ex.Message);
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
