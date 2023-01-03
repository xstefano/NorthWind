using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NorthWind.WebExceptionsPresenter
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly IDictionary<Type, IExceptionHandler> ExceptionHandlers;

		public ApiExceptionFilterAttribute(
			IDictionary<Type, IExceptionHandler> exceptionHandlers)
		{
			ExceptionHandlers = exceptionHandlers;
		}

		public override void OnException(ExceptionContext context)
		{
			Type ExceptionType = context.Exception.GetType();

			if (ExceptionHandlers.ContainsKey(ExceptionType))
			{
				ExceptionHandlers[ExceptionType].Handle(context);
			}
			else
			{
				new ExceptionHandlerBase().SetResult(context,
					StatusCodes.Status500InternalServerError,
					"An error occurred while processing the response.", "");
			}
			base.OnException(context);
		}
	}
}
