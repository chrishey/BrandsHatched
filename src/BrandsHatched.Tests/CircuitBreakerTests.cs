using BrandsHatched.CircuitBreaker.Logging;
using BrandsHatched.CircuitBreaker.Service;
using BrandsHatched.CircuitBreaker.Store;
using NSubstitute;
using Xunit;

namespace BrandsHatched.Tests
{
	public class CircuitBreakerTests
	{
		private readonly ICircuitBreakerStore _circuitBreakerStore;
		private readonly ILog _log;
		private readonly IDumbService _dumbService;

		public CircuitBreakerTests()
		{
			_circuitBreakerStore = Substitute.For<ICircuitBreakerStore>();
			_log = Substitute.For<ILog>();
			_dumbService = Substitute.For<IDumbService>();
		}

		[Fact]
		public void WhenFailedCallsThresholdIsHitCircuitBreaks()
		{
			// Given
			var itemUnderTest = new CircuitBreaker.CircuitBreaker(_circuitBreakerStore, _log);
			itemUnderTest.Configure(typeof(DumbServiceException), 1, 1);
			

			// When
			itemUnderTest.ExecuteAction(()=>_dumbService.DoSomething(false), "435b817a48dd46b78082330349906414");

			// Then
			Assert.True(itemUnderTest.IsOpen);
		}
	}
}
