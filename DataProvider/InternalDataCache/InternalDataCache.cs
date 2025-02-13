using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Host;
using DotNetNuke.Modules.DNNTokens.Components.Models;
using DotNetNuke.Modules.DNNTokens.DataProvider.DbContext;
using System;
using System.Collections.Generic;

namespace DotNetNuke.Modules.DNNTokens.DataProvider
{
	/// <summary>
	/// Provides a cache for internal data to improve performance.
	/// </summary>
	internal static class InternalDataCache
	{
		public static List<int> GetAllowedTabsByPortal(int portalId)
		{
			string cacheKey = "dnntokens_" + portalId + "_allowedtabs_";
			object cacheObject = DataCache.GetCache(cacheKey);
			if (cacheObject == null)
			{
				List<int> tabIdList = PortalTabsDb.GetAllPortalTabs(portalId);

				DataCache.SetCache(cacheKey, tabIdList, TimeSpan.FromMinutes(20 * Convert.ToInt32(Host.PerformanceSetting)));

				return tabIdList;
			}
			else
			{
				return (List<int>)cacheObject;
			}
		}

		public static GeneralPortalSettings GetGeneralPortalSettings(int portalId)
		{
			string cacheKey = "dnntokens_" + portalId + "_generalportalsettings_";
			object cacheObject = DataCache.GetCache(cacheKey);
			if (cacheObject == null)
			{
				GeneralPortalSettings generalPortalSettings = GeneralPortalSettingsDb.GetGeneralPortalSettings(portalId);

				DataCache.SetCache(cacheKey, generalPortalSettings, TimeSpan.FromMinutes(20 * Convert.ToInt32(Host.PerformanceSetting)));

				return generalPortalSettings;
			}
			else
			{
				return (GeneralPortalSettings)cacheObject;
			}
		}

		public static List<TokenViewData> GetAllPortalTokens(int portalId)
		{
			string cacheKey = "dnntokens_" + portalId + "_portaltokens_";
			object cacheObject = DataCache.GetCache(cacheKey);
			if (cacheObject == null)
			{
				List<TokenViewData> tokenViewData = TokenDb.GetAllPortalTokens(portalId);

				DataCache.SetCache(cacheKey, tokenViewData, TimeSpan.FromMinutes(20 * Convert.ToInt32(Host.PerformanceSetting)));

				return tokenViewData;
			}
			else
			{
				return (List<TokenViewData>)cacheObject;
			}
		}

		public static void ClearCacheByPortal(int portalId)
		{
			DataCache.ClearCache("dnntokens_" + portalId);
		}
	}
}