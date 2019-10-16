using System.Net;
using Administration.API.Infrastructure.Exceptions;
using Administration.API.Models.ActionResults;
using Administration.API.Models.Responses;
using Administration.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Administration.API.Infrastructure.Filter
{
	public class GlobalExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			var exception = context.Exception;
			switch (exception)
			{
				case AdministrationDomainException domainException:
				{
					var validationError = new ValidationErrorResponse(domainException.ErrorCode, domainException.Message);
				
					context.Result = new BadRequestObjectResult(validationError);
					context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
					break;
				}
				case AdministrationApplicationNotFoundException notFoundException:
				{
					var validationError = new ValidationErrorResponse(notFoundException.ErrorCode, notFoundException.Message);

					context.Result = new NotFoundObjectResult(validationError);
					context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				}
				case AdministrationApplicationException applicationException:
				{
					var validationError = new ValidationErrorResponse(applicationException.ErrorCode, applicationException.Message);

					context.Result = new BadRequestObjectResult(validationError);
					context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				}
				default:
				{
					var validationError = new ValidationErrorResponse(exception.Message);

					context.Result = new InternalServerErrorObjectResult(validationError);
					context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
					break;
				}
			}

			context.ExceptionHandled = true;

		}
	}
}
