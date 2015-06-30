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
			_dumbService.When(x=>x.DoSomething(false)).Throw(new DumbServiceException("Kaboom!!!"));
		}

		[Fact]
		public void WhenFailedCallsThresholdIsHitCircuitBreaks()
		{
			var itemUnderTest = new CircuitBreaker.CircuitBreaker(_circuitBreakerStore, _log);
			itemUnderTest.Configure(typeof(DumbServiceException), 1, 1);
			
			itemUnderTest.ExecuteAction(()=>_dumbService.DoSomething(true), "435b817a48dd46b78082330349906414");
			Assert.True(itemUnderTest.IsClosed);

			itemUnderTest.ExecuteAction(()=>_dumbService.DoSomething(false), "21af124e242c415bb8f89b4512aaef4e");
			Assert.True(itemUnderTest.IsOpen);
		}
	}
}
