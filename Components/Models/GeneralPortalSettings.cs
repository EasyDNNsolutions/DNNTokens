using System;

namespace DotNetNuke.Modules.DNNTokens.Components.Models
{
	[Serializable]
	public class GeneralPortalSettings
	{
		public int PortalId { get; set; }
		public bool RenderDNNTokens { get; set; }
		public bool ReplaceOnAllTabs { get; set; }
	}
}
