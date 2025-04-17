/// <summary>
/// Module Globals
/// </summary>
namespace DotNetNuke.Modules.DNNTokens.Common
{
	public static class Globals
	{
		public const string RegexPattern = @"\[\{([^}]+)\}\]";
		public const string PortalSettingsRegexPattern = @"\[Portal:.*?\]";
		public const string TabRegexPattern = @"\[Tab:.*?\]";
		public const string UserRegexPattern = @"\[User:.*?\]";
		public const string DateTimeRegexPattern = @"\[DateTime:.*?\]";
		public const string CultureRegexPattern = @"\[Culture:.*?\]";
	}
}