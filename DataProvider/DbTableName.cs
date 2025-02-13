namespace DotNetNuke.Modules.DNNTokens.DataProvider
{
	internal static class DbTableName
	{
		internal static readonly string Token = SqlDataProvider.ModuleTableNameWrapper("Token");
		internal static readonly string Category = SqlDataProvider.ModuleTableNameWrapper("Category");
		internal static readonly string GeneralPortalSettings = SqlDataProvider.ModuleTableNameWrapper("GeneralPortalSettings");
		internal static readonly string PortalTabs = SqlDataProvider.ModuleTableNameWrapper("PortalTabs");
	}
}