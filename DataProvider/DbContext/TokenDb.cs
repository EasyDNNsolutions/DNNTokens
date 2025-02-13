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
		public static List<TokenViewData> GetPortalTokens(int portalId, int maximumRows, int startRowIndex)
		{
			List<TokenViewData> tokenList = new List<TokenViewData>();

			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand(@"
WITH TokensAll AS (
SELECT ROW_NUMBER() OVER (
			ORDER BY tokens.id ASC
			) AS ComPos
		, tokens.*, category.Name AS CategoryName FROM " + DbTableName.Token + @" AS tokens
			LEFT JOIN " + DbTableName.Category + @" AS category ON tokens.categoryId = category.Id
WHERE tokens.portalId=@portalId
)
SELECT *
FROM TokensAll
WHERE ComPos BETWEEN @startRowIndex AND @maximumRows;
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
								TokenViewData tokenViewData = new TokenViewData()
								{
									Id = (int)reader["Id"],
									PortalId = (int)reader["PortalId"],
									Name = Convert.ToString(reader["Name"]),
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

		public static int GetTotalNumberOfTokensByPortalId(int portalId, int maximumRows, int startRowIndex)
		{
			using (SqlConnection connection = SqlDataProvider.GetSqlConnection())
			{
				using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + DbTableName.Token + " WHERE portalId=@portalId ", connection))
				{
					command.Parameters.Add("@portalId", SqlDbType.Int).Value = portalId;
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

	}
}