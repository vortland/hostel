using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Administration.API.Models.ActionResults
{
	public class InternalServerErrorObjectResult : ObjectResult
	{
		public InternalServerErrorObjectResult(object error)
			: base(error)
		{
			StatusCode = StatusCodes.Status500InternalServerError;
		}
	}
}
