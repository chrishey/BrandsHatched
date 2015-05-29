using System;
using System.Threading.Tasks;
using BrandsHatched.CircuitBreaker;
using BrandsHatched.CircuitBreaker.Service;
using BrandsHatched.Web.ViewModel;
using Nancy;

namespace BrandsHatched.Web.Modules
{
	public class HomeModule : NancyModule
	{
		private readonly ICircuitBreaker _circuitBreaker;
		private readonly IDumbService _dumbService;

		public HomeModule(ICircuitBreaker circuitBreaker, IDumbService dumbService)
		{
			_circuitBreaker = circuitBreaker;
			_dumbService = dumbService;

			Get["/"] = _ => GetCurrentState();
			Get["/success"] = _ => GetTriggerSuccess();
			Get["/failure"] = _ => GetTriggerFailure();
		}

		private dynamic GetTriggerSuccess()
		{
			var model = new CircuitBreakerModel();
			_circuitBreaker.ExecuteAction(()=>_dumbService.DoSomething(true));
			model.State = _circuitBreaker.State;
			model.LastStateChange = _circuitBreaker.StateChanged.ToString("F");
			return View["BreakerState", model];
		}

		private dynamic GetTriggerFailure()
		{
			var model = new CircuitBreakerModel();
			_circuitBreaker.ExecuteAction(()=>_dumbService.DoSomething(false));
			model.State = _circuitBreaker.State;
			model.LastStateChange = _circuitBreaker.StateChanged.ToString("F");
			return View["BreakerState", model];
		}

		private dynamic GetCurrentState()
		{
			return View["Home"];
		}
	}
}