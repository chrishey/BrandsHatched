using System.Net;
using System.Web.Mvc;

namespace BrandsHatched.Mvc
{
	public class CircuitBreakerAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			

			filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
		}
	}
}