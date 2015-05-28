using System;
using BrandsHatched.CircuitBreaker.Logging;

namespace BrandsHatched.CircuitBreaker.Store
{
	public interface ICircuitBreakerStore
	{
		CircuitBreakerState CurrentState { get; }
		DateTime StateLastChanged { get; }
		void Trip(Exception exception);
		void Reset();
	}

	class CircuitBreakerStore : ICircuitBreakerStore
	{
		private readonly ILog _log;

		public CircuitBreakerStore(ILog log)
		{
			_log = log;
			_currentState = CircuitBreakerState.Closed;
		}

		private CircuitBreakerState _currentState;
		private DateTime _stateLastChanged;

		public CircuitBreakerState CurrentState
		{
			get { return _currentState; }
		}

		public DateTime StateLastChanged
		{
			get { return _stateLastChanged; }
		}

		public void Trip(Exception exception)
		{
			_currentState = CircuitBreakerState.Open;
			_stateLastChanged = DateTime.Now;
			_log.Log(exception.Message);
		}

		/// <summary>
		/// This is used as a force reset, once the circuit is brought into a half open state transferring to closed will be dealt with elsewhere.
		/// </summary>
		public void Reset()
		{
			_currentState = CircuitBreakerState.Closed;
			_stateLastChanged = DateTime.Now;
		}
	}
}