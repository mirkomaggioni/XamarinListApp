using System;
using System.Xml.Serialization;

namespace ListApp.BusinessObjects
{
	[Serializable()]
	[XmlRoot("settings")]
	public class AppSettings
	{
		[XmlElement("serverAppUrl")]
		public string ServerAppUrl { get; set; }
	}
}