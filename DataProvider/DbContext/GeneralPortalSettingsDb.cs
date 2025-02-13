using DotNetNuke.Modules.DNNTokens.Components.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.DNNTokens.DataProvider.DbContext
{
	/// <summary>
	/// Data Provider for Categories
	/// </summary>
	internal static class GeneralPortalSettingsDb
	{
		/// <summary>
		/// Returns a list of categroies within a portal
		/// </summary>
		public static GeneralPortalSettings GetGeneralPortalSettings(int portalId)
		{
			GeneralPortalSettings generalPortalSettings = null;

			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"SELECT * FROM " + DbTableName.GeneralPortalSettings + " WHERE portalId=@portalId", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								generalPortalSettings = new GeneralPortalSettings
								{
									PortalId = (int)reader["PortalId"],
									RenderDNNTokens = (bool)reader["RenderDNNTokens"],
									ReplaceOnAllTabs = (bool)reader["ReplaceOnAllTabs"]
								};
							}
						}
					}
				}
			}

			return generalPortalSettings;
		}

		public static void SaveGeneralPortalSettings(GeneralPortalSettings generalPortalSettings)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(
@"IF EXISTS(SELECT 1 FROM " + DbTableName.GeneralPortalSettings + @" WHERE PortalId=@PortalId)
UPDATE" + DbTableName.GeneralPortalSettings + @"
   SET
	[RenderDNNTokens] = @RenderDNNTokens
	,[ReplaceOnAllTabs] = @ReplaceOnAllTabs
 WHERE PortalId = @PortalId
ELSE
INSERT INTO " + DbTableName.GeneralPortalSettings + @"
(
[PortalId]
,[RenderDNNTokens]
,[ReplaceOnAllTabs]
)
	 VALUES
 (
 @PortalId
,@RenderDNNTokens
,@ReplaceOnAllTabs
);", connection))
				{
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = generalPortalSettings.PortalId;
					command.Parameters.Add("@RenderDNNTokens", SqlDbType.Bit).Value = generalPortalSettings.RenderDNNTokens;
					command.Parameters.Add("@ReplaceOnAllTabs", SqlDbType.Bit).Value = generalPortalSettings.ReplaceOnAllTabs;
					command.ExecuteNonQuery();
				}
			}
		}
	}
}