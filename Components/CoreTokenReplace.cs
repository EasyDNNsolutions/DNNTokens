
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Users;

namespace DotNetNuke.Modules.DNNTokens.Components
{
	public class CoreTokenReplace
	{
		public static string ReplacePortalToken(PortalSettings portalSettings, string sourceString, string tokenToReplace, string tokenValue)
		{
			switch (tokenValue.ToLowerInvariant())
			{
				case "currency":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.Currency.ToString());
					break;
				case "defaultlanguage":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.DefaultLanguage);
					break;
				case "description":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.Description);
					break;
				case "email":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.Email);
					break;
				case "expirydate":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.ExpiryDate.ToString());
					break;
				case "footertext":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.FooterText);
					break;
				case "hometabid":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.HomeTabId.ToString());
					break;
				case "keywords":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.KeyWords);
					break;
				case "logintabid":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.LoginTabId.ToString());
					break;
				case "logofile":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.LogoFile);
					break;
				case "portalid":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.PortalId.ToString());
					break;
				case "portalname":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.PortalName);
					break;
				case "homedirectory":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.HomeDirectory);
					break;
				case "splashtabid":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.SplashTabId.ToString());
					break;
				case "url":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.PortalAlias.HTTPAlias);
					break;
				case "portalalias":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.PortalAlias.HTTPAlias);
					break;
				case "userregistration":
					sourceString = sourceString.Replace(tokenToReplace, portalSettings.UserRegistration.ToString());
					break;
			}
			return sourceString;
		}

		public static string ReplaceTabToken(TabInfo tabInfo, string sourceString, string tokenToReplace, string tokenValue)
		{
			switch (tokenValue.ToLowerInvariant())
			{
				case "description":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.Description);
					break;
				case "fullurl":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.FullUrl);
					break;
				case "iconfile":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.IconFile);
					break;
				case "keywords":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.KeyWords);
					break;
				case "level":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.Level.ToString());
					break;
				case "pageheadtext":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.PageHeadText);
					break;
				case "parentid":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.ParentId.ToString());
					break;
				case "portalid":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.PortalID.ToString());
					break;
				case "tabid":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.TabID.ToString());
					break;
				case "tabname":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.TabName);
					break;
				case "tabpath":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.TabPath);
					break;
				case "title":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.Title);
					break;
				case "url":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.Url);
					break;
				case "startdate":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.StartDate.ToString());
					break;
				case "enddate":
					sourceString = sourceString.Replace(tokenToReplace, tabInfo.EndDate.ToString());
					break;
			}
			return sourceString;
		}

		public static string ReplaceUserToken(UserInfo userInfo, string sourceString, string tokenToReplace, string tokenValue)
		{
			switch (tokenValue.ToLowerInvariant())
			{
				case "displayname":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.DisplayName);
					break;
				case "email":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.Email);
					break;
				case "firstname":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.FirstName);
					break;
				case "fullname":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.DisplayName);
					break;
				case "lastname":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.LastName);
					break;
				case "portalid":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.PortalID.ToString());
					break;
				case "userid":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.UserID.ToString());
					break;
				case "username":
					sourceString = sourceString.Replace(tokenToReplace, userInfo.Username);
					break;
			}
			return sourceString;
		}
	}
}
