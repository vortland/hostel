using System.Xml.Serialization;

namespace Administration.API.Models.Responses.Base
{
	public class Link
	{
		public Link()
		{
		}

		public Link(string href, string rel)
		{
			Href = href;
			Rel = rel;
		}

		[XmlAttribute] public string Rel { get; set; }

		[XmlAttribute] public string Href { get; set; }
	}
}
