using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.DNNTokens.DataProvider.DbContext
{

	internal static class PortalTabsDb
	{
		/// <summary>
		/// Returns a list of selected portal tabs
		/// </summary>
		public static List<int> GetAllPortalTabs(int portalId)
		{
			List<int> tabIdList = new List<int>();
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
SELECT * FROM " + DbTableName.PortalTabs + @" WHERE PortalId=@portalId
", connection))
				{
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = portalId;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								tabIdList.Add(
									(int)reader["TabId"]
									);
							}
						}
					}
				}
			}

			return tabIdList;
		}

		/// <summary>
		/// Save portalid and tab it to PortalTabs
		/// </summary>
		/// <param name="portalId"></param>
		/// <param name="tabId"></param>
		public static void SavePortalTab(int portalId, int tabId)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(
@"
INSERT INTO " + DbTableName.PortalTabs + @"
(
[PortalId]
,[TabId]
)
	VALUES
 (
 @PortalId
,@TabId
);", connection))
				{

					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = portalId;
					command.Parameters.Add("@TabId", SqlDbType.Int).Value = tabId;
					command.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Deletes all portal tabs associated with the specified portal ID.
		/// </summary>
		/// <param name="portalId">The ID of the portal for which to delete tabs.</param>
		public static void DeleteAllPortalTabs(int portalId)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("DELETE FROM " + DbTableName.PortalTabs + @" WHERE PortalId=@PortalId", connection))
				{
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = portalId;
					command.ExecuteNonQuery();
				}
			}
		}

	}
}