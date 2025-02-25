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
	internal static class CategoryDb
	{
		/// <summary>
		/// Returns a list of categroies within a portal
		/// </summary>
		public static List<Category> GetPortalCategories(int portalId, int maximumRows, int startRowIndex)
		{
			List<Category> categoryList = new List<Category>();

			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
WITH CategoryAll AS (
SELECT ROW_NUMBER() OVER (
			ORDER BY category.id ASC
			) AS ComPos
		, category.* FROM " + DbTableName.Category + @" AS category WHERE category.portalId=@portalId
)
SELECT*
FROM CategoryAll
WHERE ComPos BETWEEN @startRowIndex
		AND @maximumRows;
", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
					command.Parameters.Add("@maximumRows", SqlDbType.Int).Value = startRowIndex + maximumRows;
					command.Parameters.Add("@startRowIndex", SqlDbType.Int).Value = startRowIndex + 1;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								categoryList.Add(MapReaderToCategory(reader));
							}
						}
					}
				}
			}

			return categoryList;
		}

		/// <summary>
		/// Returns a category count within a portal
		/// </summary>
		/// <param name="portalId"></param>
		/// <param name="maximumRows"></param>
		/// <param name="startRowIndex"></param>
		/// <returns></returns>
		public static int GetTotalNumberOfCategoriesByPortalId(int portalId, int maximumRows, int startRowIndex)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + DbTableName.Category + " WHERE portalId=@portalId ", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
					return (int)command.ExecuteScalar();
				}
			}
		}

		/// <summary>
		/// Returns a list of categories within a portal
		/// </summary>
		public static List<Category> GetAllPortalCategories(int portalId)
		{
			List<Category> categoryList = new List<Category>();
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
SELECT category.* FROM " + DbTableName.Category + @" AS category WHERE category.portalId=@portalId
", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								categoryList.Add(MapReaderToCategory(reader));
							}
						}
					}
				}
			}

			return categoryList;
		}

		/// <summary>
		/// Saves a category to the database. If the category already exists, it updates the existing record.
		/// Otherwise, it inserts a new record.
		/// </summary>
		/// <param name="category">The category to save.</param>
		public static int SaveCategory(Category category)
		{
			int insertedId	= 0;
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(
@"IF EXISTS(SELECT 1 FROM " + DbTableName.Category + @" WHERE Id=@Id) AND @Id <> 0
BEGIN
UPDATE" + DbTableName.Category + @"
   SET
	[PortalId] = @PortalId
	,[Name] = @Name
 WHERE Id = @Id
SELECT @Id
END
ELSE
BEGIN
INSERT INTO " + DbTableName.Category + @"
(
[PortalId]
,[Name]
)
    VALUES
 (
 @PortalId
,@Name
)
SELECT SCOPE_IDENTITY();
END", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = category.Id;
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = category.PortalId;
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = category.Name;

					object resObject = command.ExecuteScalar();
					if (resObject != DBNull.Value)
						insertedId = Convert.ToInt32(resObject);
				}
			}
			return insertedId;
		}

		/// <summary>
		/// Deletes a category by its ID.
		/// </summary>
		/// <param name="id">The ID of the category to delete.</param>
		public static void DeleteCategory(int id)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("DELETE FROM " + DbTableName.Category + @" WHERE Id=@Id", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
					command.ExecuteNonQuery();
				}
			}
		}

		/// <summary>
		/// Returns a category by name
		/// </summary>
		/// <param name="portalId"></param>
		/// <param name="categoryName"></param>
		/// <returns></returns>
		public static Category GetCategoryByName(int portalId, string categoryName)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT * FROM " + DbTableName.Category + " WHERE Name=@Name AND PortalId=@PortalId", connection))
				{
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = categoryName;
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = portalId;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								return new Category
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"])
								};
							}
						}
						return null;
					}
				}
			}
		}

		/// <summary>
		/// check if category exists in tokens table
		/// </summary>
		/// <param name="id">categoryid</param>
		/// <returns></returns>
		/// 
		public static bool CategoryInUse(int id)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + DbTableName.Token + " WHERE CategoryId=@Id", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
					return (int)command.ExecuteScalar() > 0;
				}
			}
		}

		public static void UpdateCategory(int id, string name)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("UPDATE " + DbTableName.Category + " SET Name=@Name WHERE Id=@Id", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = name;
					command.ExecuteNonQuery();
				}
			}
		}
		private static Category MapReaderToCategory(SqlDataReader reader)
		{
			return new Category()
			{
				Id = (int)reader["Id"],
				PortalId = (int)reader["PortalId"],
				Name = Convert.ToString(reader["Name"]),
			};
		}
	}
}