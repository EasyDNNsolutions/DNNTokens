
using DotNetNuke.Modules.DNNTokens.Components.Enums;
using System;

namespace DotNetNuke.Modules.DNNTokens.Components.Models
{
	/// <summary>
	/// Teoken class
	/// </summary>
	[Serializable]
	public class Token
	{
		public int Id { get; set; }
		public int PortalId { get; set; }
		public string Name { get; set; }
		public string TokenValue { get; set; }
		public int? CategoryId { get; set; }
		public TokenType TokenType { get; set; }
		public TokenScope Scope { get; set; }
		public int? UserId { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
