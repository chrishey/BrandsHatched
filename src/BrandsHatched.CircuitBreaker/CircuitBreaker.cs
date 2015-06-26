using System;
using System.Configuration;
using BrandsHatched.CircuitBreaker.Configuration;
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
	    private readonly Policy _policy;
	    private Type _exceptionToHandle;
	    private int _waitTimeInMinutes;
	    private int _failureThreshold;

	    public CircuitBreaker(ICircuitBreakerStore circuitBreakerStore, ILog log)
	    {
		    _circuitBreakerStore = circuitBreakerStore;
		    _log = log;
		    _policy = Policy.Handle<DumbServiceException>().CircuitBreaker(FailedCallThreshold, WaitTimeBeforeHalfOpen);
	    }

	    public void Configure(Type exceptionToHandle, int failureThreshold, int waitTimeInMinutes)
	    {
		    _exceptionToHandle = exceptionToHandle;
		    _failureThreshold = failureThreshold;
		    _waitTimeInMinutes = waitTimeInMinutes;
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

	  
	    public void ExecuteAction(Action action, string key)
	    {
		    if (_circuitBreakerStore.BrokenCircuits.CircuitBrokenForUser(key))
			    return;

		    try
		    {
			    _policy.Execute(action);
			    if (IsOpen)
				    _circuitBreakerStore.Reset();
		    }
		    catch (BrokenCircuitException exception)
		    {
			    _circuitBreakerStore.Trip(exception);
		    }
		    catch (Exception exception)
		    {
				_log.Log(exception.Message);
		    }
	    }

	    public int FailedCallThreshold
	    {
		    get { return _failureThreshold; }
	    }

	    public TimeSpan WaitTimeBeforeHalfOpen
	    {
		    get { return TimeSpan.FromMinutes(_waitTimeInMinutes); }
	    }
    }
}
