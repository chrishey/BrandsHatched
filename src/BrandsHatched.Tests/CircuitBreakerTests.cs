using BrandsHatched.CircuitBreaker.Logging;
using BrandsHatched.CircuitBreaker.Service;
using BrandsHatched.CircuitBreaker.Store;
using NSubstitute;
using Xunit;

namespace BrandsHatched.Tests
{
	public class CircuitBreakerTests
	{
		private ICircuitBreakerStore _circuitBreakerStore;
		private IBrokenCircuits _brokenCircuits;
		private ILog _log;
		private IDumbService _dumbService;

		public CircuitBreakerTests()
		{
			_circuitBreakerStore = Substitute.For<ICircuitBreakerStore>();
			_brokenCircuits = Substitute.For<IBrokenCircuits>();
			_log = Substitute.For<ILog>();
			_dumbService = Substitute.For<IDumbService>();
		}

		[Fact]
		public void WhenFailedCallsThresholdIsHitCircuitBreaks()
		{
			// Given
			var itemUnderTest = new CircuitBreaker.CircuitBreaker(_circuitBreakerStore, _log);
			

			// When
			itemUnderTest.ExecuteAction(()=>_dumbService.DoSomething(false), "435b817a48dd46b78082330349906414");

			// Then
			Assert.True(itemUnderTest.IsOpen);
		}
	}
}
