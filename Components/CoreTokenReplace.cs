
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Users;
using System.Globalization;
using System;
using DotNetNuke.Services.Tokens;

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

		public static string ReplaceDateTimeToken(UserInfo userInfo, string sourceString, string tokenToReplace, string tokenValue, CultureInfo ciLanguage)
		{
			string format = string.Empty;
			if (tokenValue.Contains("|"))
			{
				string[] tokens = tokenValue.Split('|');
				tokenValue = tokens[0];
				format = tokens[1];
			}
			TimeZoneInfo userTimeZone = userInfo.Profile.PreferredTimeZone;
		
			switch (tokenValue.ToLowerInvariant())
			{
				case "current":
					{
						if (format == string.Empty)
						{
							format = "D";
						}

						sourceString = sourceString.Replace(tokenToReplace, TimeZoneInfo.ConvertTime(DateTime.Now, userTimeZone).ToString(format, ciLanguage));
						break;
					}
				case "now":
					{
						if (format == string.Empty)
						{
							format = "g";
						}

						sourceString = sourceString.Replace(tokenToReplace, TimeZoneInfo.ConvertTime(DateTime.Now, userTimeZone).ToString(format, ciLanguage));
						break;
					}
				case "system":
					if (format == string.Empty)
					{
						format = "g";
					}

					return DateTime.Now.ToString(format, ciLanguage);
				case "utc":
					if (format == string.Empty)
					{
						format = "g";
					}

					return DateTime.Now.ToUniversalTime().ToString(format, ciLanguage);
				default:
					
				break;
			}
			return sourceString;
		}

		public static string ReplaceCultureToken(UserInfo userInfo, string sourceString, string tokenToReplace, string tokenValue, CultureInfo ciLanguage)
		{
			switch (tokenValue.ToLowerInvariant())
			{
				case "englishname":
					{
						sourceString = sourceString.Replace(tokenToReplace, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ciLanguage.EnglishName));
						break;
					}
				case "lcid":
					{
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.LCID.ToString());
						break;
					}
				case "name":
					{
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.Name);
						break;
					}
				case "nativename":
					{
						sourceString = sourceString.Replace(tokenToReplace, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ciLanguage.NativeName));
						break;
					}
				case "twoletterisocode":
					{
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.TwoLetterISOLanguageName);
						break;
					}
				case "threeletterisocode":
					{
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.ThreeLetterISOLanguageName);
						break;
					}
				case "displayname":
					{
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.DisplayName);
						break;
					}
				case "countryname":
					{
						if (ciLanguage.IsNeutralCulture)
						{
							sourceString = sourceString.Replace(tokenToReplace, string.Empty);
						}
						else
						{
							RegionInfo country = new RegionInfo(new CultureInfo(ciLanguage.Name, false).LCID);
							sourceString = sourceString.Replace(tokenToReplace, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(country.EnglishName));
						}
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.DisplayName);
						break;
					}
				case "countrynativename":
					{
						if (ciLanguage.IsNeutralCulture)
						{
							sourceString = sourceString.Replace(tokenToReplace, string.Empty);
						}
						else
						{
							RegionInfo country = new RegionInfo(new CultureInfo(ciLanguage.Name, false).LCID);
							sourceString = sourceString.Replace(tokenToReplace, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(country.NativeName));
						}
						sourceString = sourceString.Replace(tokenToReplace, ciLanguage.DisplayName);
						break;
					}
				default:

					break;
			}
			return sourceString;
		}
	}
}
