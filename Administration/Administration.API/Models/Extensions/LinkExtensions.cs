using System.Collections.Generic;
using Administration.API.Models.Responses.Base;

namespace Administration.API.Models.Extensions
{
	public static class LinkExtensions
	{
		public static RestResponse WithLinks(this RestResponse response, params Link[] links)
		{
			if (response.Links == null)
			{
				response.Links = new List<Link>();
			}

			response.Links.AddRange(links);
			return response;
		}
	}
}
