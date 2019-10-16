using System.Collections.Generic;

namespace Administration.API.Models.Responses.Base
{
	public abstract class RestResponse
	{
		public int? Id { get; set; }
		public List<Link> Links { get; set; }
	}
}
