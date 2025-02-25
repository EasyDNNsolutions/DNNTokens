using DotNetNuke.Modules.DNNTokens.Components.Enums;
using DotNetNuke.Modules.DNNTokens.Components.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.DNNTokens.DataProvider.DbContext
{
	/// <summary>
	/// Data Provider for Tokens
	/// </summary> 
	internal static class TokenDb
	{
		/// <summary>
		/// Returns a list of tokens within a portal with pagination
		/// </summary>
		public static List<TokenViewData> GetPortalTokens(int portalId, int maximumRows, int startRowIndex, string sortTerm, string searchTerm)
		{
			List<TokenViewData> tokenList = new List<TokenViewData>();
			string sortBy = " tokens.id ASC ";
			if (!string.IsNullOrEmpty(sortTerm))
			{
				if (sortTerm == "1")
					sortBy = " tokens.id DESC ";
				else if (sortTerm == "2")
					sortBy = " tokens.name ASC ";
				else if (sortTerm == "3")
					sortBy = " tokens.name DESC ";
				else if (sortTerm == "4")
					sortBy = " category.Name ASC ";
				else if (sortTerm == "5")
					sortBy = " category.Name DESC ";
			}
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
WITH TokensAll AS (
SELECT ROW_NUMBER() OVER (
			ORDER BY " + sortBy + @"
			) AS ComPos
		, tokens.*, category.Name AS CategoryName FROM " + DbTableName.Token + @" AS tokens
			LEFT JOIN " + DbTableName.Category + @" AS category ON tokens.categoryId = category.Id
WHERE tokens.portalId=@portalId" + (string.IsNullOrEmpty(searchTerm) ? "" : " AND (tokens.name LIKE @searchTerm OR tokens.description LIKE @searchTerm OR tokens.tokenvalue LIKE @searchTerm)") + @"
)
SELECT *
FROM TokensAll
WHERE ComPos BETWEEN @startRowIndex AND @maximumRows;
", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
					command.Parameters.Add("@maximumRows", SqlDbType.Int).Value = startRowIndex + maximumRows;
					command.Parameters.Add("@startRowIndex", SqlDbType.Int).Value = startRowIndex + 1;
					command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								TokenViewData tokenViewData = new TokenViewData()
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"]),
									Description = Convert.ToString(reader["Description"]),
									TokenValue = Convert.ToString(reader["TokenValue"]),
									TokenType = (TokenType)(int)reader["TokenType"],
									Scope = (TokenScope)(int)reader["Scope"],
									UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
									CategoryId = reader["CategoryId"] == DBNull.Value ? null : (int?)reader["CategoryId"],
									CategoryName = reader["CategoryName"] == DBNull.Value ? null : Convert.ToString(reader["CategoryName"]),
									DateCreated = Convert.ToDateTime(reader["DateCreated"])
								};
								tokenList.Add(tokenViewData);
							}
						}
					}
				}
			}

			return tokenList;
		}

		/// <summary>
		/// Returns a list of tokens within a portal
		/// </summary>
		public static List<TokenViewData> GetAllPortalTokens(int portalId)
		{
			List<TokenViewData> tokenList = new List<TokenViewData>();
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
SELECT tokens.*, category.Name AS CategoryName FROM " + DbTableName.Token + @" AS tokens
LEFT JOIN " + DbTableName.Category + @" AS category ON tokens.categoryId = category.Id
WHERE tokens.portalId=@portalId OR Scope = 1
", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								TokenViewData tokenViewData = new TokenViewData()
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"]),
									Description = Convert.ToString(reader["Description"]),
									TokenValue = Convert.ToString(reader["TokenValue"]),
									TokenType = (TokenType)(int)reader["TokenType"],
									Scope = (TokenScope)(int)reader["Scope"],
									UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
									CategoryId = reader["CategoryId"] == DBNull.Value ? null : (int?)reader["CategoryId"],
									CategoryName = reader["CategoryName"] == DBNull.Value ? null : Convert.ToString(reader["CategoryName"]),
									DateCreated = Convert.ToDateTime(reader["DateCreated"])
								};
								if (!string.IsNullOrEmpty(tokenViewData.CategoryName))
								{
									tokenViewData.CategoryAndToken = tokenViewData.CategoryName + ":" + tokenViewData.Name;
								}
								else
								{
									tokenViewData.CategoryAndToken = tokenViewData.Name;
								}
								tokenList.Add(tokenViewData);
							}
						}
					}
				}
			}

			return tokenList;
		}

		public static int GetTotalNumberOfTokensByPortalId(int portalId, int maximumRows, int startRowIndex, string sortTerm, string searchTerm)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + DbTableName.Token + " WHERE portalId=@portalId " + (string.IsNullOrEmpty(searchTerm) ? "" : " AND (name LIKE @searchTerm OR description LIKE @searchTerm OR tokenvalue LIKE @searchTerm)"), connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
					command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
					return (int)command.ExecuteScalar();
				}
			}
		}

		public static TokenViewData GetTokenById(int id)
		{
			TokenViewData tokenViewData = null;
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT tokens.*, category.Name AS CategoryName FROM " + DbTableName.Token + @" AS tokens 
				LEFT JOIN " + DbTableName.Category + @" AS category ON tokens.categoryId = category.Id
				WHERE tokens.Id=@Id", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								tokenViewData = new TokenViewData()
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"]),
									Description = Convert.ToString(reader["Description"]),
									TokenValue = Convert.ToString(reader["TokenValue"]),
									TokenType = (TokenType)(int)reader["TokenType"],
									Scope = (TokenScope)(int)reader["Scope"],
									UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
									CategoryId = reader["CategoryId"] == DBNull.Value ? null : (int?)reader["CategoryId"],
									CategoryName = reader["CategoryName"] == DBNull.Value ? null : Convert.ToString(reader["CategoryName"]),
									DateCreated = Convert.ToDateTime(reader["DateCreated"])
								};
								if (!string.IsNullOrEmpty(tokenViewData.CategoryName))
								{
									tokenViewData.CategoryAndToken = tokenViewData.CategoryName + ":" + tokenViewData.Name;
								}
								else
								{
									tokenViewData.CategoryAndToken = tokenViewData.Name;
								}
							}
						}
					}
				}
			}

			return tokenViewData;
		}
		public static void InsertUpdate(Token token)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(
@"IF EXISTS(SELECT 1 FROM " + DbTableName.Token + @" WHERE Id=@Id) AND @Id <> 0
UPDATE" + DbTableName.Token + @"
   SET
	[PortalId] = @PortalId
	,[Name] = @Name
	,[Description] = @Description
	,[TokenValue] = @TokenValue
	,[TokenType] = @TokenType
	,[Scope] = @Scope
	,[CategoryId] = @CategoryId
 WHERE Id = @Id
ELSE
INSERT INTO " + DbTableName.Token + @"
(
[PortalId]
,[Name]
,[Description]
,[TokenValue]
,[TokenType]
,[Scope]
,[UserId]
,[DateCreated]
,[CategoryId]
)
     VALUES
 (
 @PortalId
,@Name
,@Description
,@TokenValue
,@TokenType
,@Scope
,@UserId
,@DateCreated
,@CategoryId
);", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = token.Id;
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = token.PortalId;
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = token.Name;
					if (token.Description == null)
						command.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = DBNull.Value;
					else
						command.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = token.Description;

					command.Parameters.Add("@TokenValue", SqlDbType.NVarChar, -1).Value = token.TokenValue;

					if (token.CategoryId == null)
						command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = DBNull.Value;
					else
						command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = token.CategoryId;

					command.Parameters.Add("@TokenType", SqlDbType.Int).Value = (int)token.TokenType;
					command.Parameters.Add("@Scope", SqlDbType.Int).Value = (int)token.Scope;
					command.Parameters.Add("@UserId", SqlDbType.Int).Value = token.UserId;
					command.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.UtcNow;
					command.ExecuteNonQuery();
				}
			}
		}


		public static void DeleteToken(int id)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("DELETE FROM " + DbTableName.Token + @" WHERE Id=@Id", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
					command.ExecuteNonQuery();
				}
			}
		}

		//// Get token by name and portal id
		public static Token GetTokenByName(int portalId, string Name)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT * FROM " + DbTableName.Token + " WHERE Name=@Name AND PortalId=@PortalId", connection))
				{
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = Name;
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = portalId;

					using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								return new Token
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"]),
									Description = Convert.ToString(reader["Description"]),
									TokenValue = Convert.ToString(reader["TokenValue"]),
									TokenType = (TokenType)(int)reader["TokenType"],
									UserId = reader["UserId"] == DBNull.Value ? null : (int?)reader["UserId"],
									DateCreated = Convert.ToDateTime(reader["DateCreated"])
								};
							}
						}
						return null;
					}
				}
			}
		}
		public static DataSet GetPortalTokens(int portalId)
		{
			DataSet ds = new DataSet();
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
SELECT tokens.*, category.Name AS CategoryName FROM " + DbTableName.Token + @" AS tokens
LEFT JOIN " + DbTableName.Category + @" AS category ON tokens.categoryId = category.Id
WHERE tokens.portalId=@portalId OR Scope = 1
", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;

					SqlDataAdapter adapter = new SqlDataAdapter(command);
					adapter.Fill(ds, "PortalTokens Id " + portalId);
				}
			}
			return ds;
		}

		public static int InsertUpdateByName(Token token)
		{
			int result = -1;
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(
@"IF EXISTS(SELECT 1 FROM " + DbTableName.Token + @" WHERE Name=@Name AND PortalId=@PortalId)
BEGIN
UPDATE" + DbTableName.Token + @"
   SET
	[PortalId] = @PortalId
	,[Name] = @Name
	,[Description] = @Description
	,[TokenValue] = @TokenValue
	,[TokenType] = @TokenType
	,[Scope] = @Scope
	,[CategoryId] = @CategoryId
	WHERE Name=@Name AND PortalId=@PortalId
	SELECT 0;
END
ELSE
BEGIN
INSERT INTO " + DbTableName.Token + @"
(
[PortalId]
,[Name]
,[Description]
,[TokenValue]
,[TokenType]
,[Scope]
,[UserId]
,[DateCreated]
,[CategoryId]
)
     VALUES
 (
 @PortalId
,@Name
,@Description
,@TokenValue
,@TokenType
,@Scope
,@UserId
,@DateCreated
,@CategoryId
)
SELECT 1
END
;", connection))
				{
					command.Parameters.Add("@Id", SqlDbType.Int).Value = token.Id;
					command.Parameters.Add("@PortalId", SqlDbType.Int).Value = token.PortalId;
					command.Parameters.Add("@Name", SqlDbType.NVarChar, 250).Value = token.Name;
					if (token.Description == null)
						command.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = DBNull.Value;
					else
						command.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = token.Description;

					command.Parameters.Add("@TokenValue", SqlDbType.NVarChar, -1).Value = token.TokenValue;

					if (token.CategoryId == null)
						command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = DBNull.Value;
					else
						command.Parameters.Add("@CategoryId", SqlDbType.Int).Value = token.CategoryId;

					command.Parameters.Add("@TokenType", SqlDbType.Int).Value = (int)token.TokenType;
					command.Parameters.Add("@Scope", SqlDbType.Int).Value = (int)token.Scope;
					command.Parameters.Add("@UserId", SqlDbType.Int).Value = token.UserId;
					command.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.UtcNow;
					object resObject = command.ExecuteScalar();
					if (resObject != DBNull.Value)
						result = Convert.ToInt32(resObject);
				}
			}
			return result;
		}

	}
}