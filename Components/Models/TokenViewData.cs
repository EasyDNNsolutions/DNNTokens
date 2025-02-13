using System;

namespace DotNetNuke.Modules.DNNTokens.Components.Models
{
	/// <summary>
	/// Represents a token view data, extending the Token class.
	/// </summary>
	[Serializable]
	public class TokenViewData : Token
	{
		public string CategoryName { get; set; }
		public string CategoryAndToken { get; set; }

	}
}
