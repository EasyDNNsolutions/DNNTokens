using System;

namespace DotNetNuke.Modules.DNNTokens.Components.Models
{
	/// <summary>
	/// Represents a category entity.
	/// </summary>
	[Serializable]
	class Category
	{
		public int Id { get; set; }
		public int PortalId { get; set; }
		public string Name { get; set; }
	}
}
