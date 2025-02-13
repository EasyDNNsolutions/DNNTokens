using DotNetNuke.Modules.DNNTokens.DataProvider;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DotNetNuke.Modules.DNNTokens.Components.Sql
{
	public static class SQLExecute
	{
		public static DataTable GetSQLResults(string syncSQLCommand,  ref string errorMessage)
		{
			string customConnectionString = string.Empty;
			string objectQualifier = SqlDataProvider.GetObjectQualifier();
			DataSet dsResult = new DataSet();
			try
			{
				SqlConnection connection = null;
				if (string.IsNullOrEmpty(customConnectionString))
					connection = SqlDataProvider.GetSqlConnection();
				else
				{
					connection = new SqlConnection(customConnectionString);
					connection.Open();
				}
				syncSQLCommand = syncSQLCommand.Replace("{objectQualifier}", objectQualifier);
				SqlCommand command = new SqlCommand(syncSQLCommand, connection);
				SqlDataAdapter adapter = new SqlDataAdapter(command);
				adapter.Fill(dsResult, "Data");
				connection.Close();
			}
			catch (Exception exception)
			{
				errorMessage = exception.Message;
			}
			if (dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
			{
				return dsResult.Tables[0];
			}
			else
				return null;
		}

		public static bool TestSQLConnectionString(string connectionString, ref string errorMessage)
		{
			bool connectionValid = true;
			try
			{
				SqlConnection connection = new SqlConnection(connectionString);
				connection.Open();
				connection.Close();
			}
			catch (Exception exception)
			{
				errorMessage = exception.Message;
				connectionValid = false;
			}
			return connectionValid;
		}

		//Print out the content of DataTable
		public static string RenderSQLDataTableResults(DataTable dataTable)
		{
			string html = "";
			foreach (DataRow row in dataTable.Rows)
			{
				foreach (DataColumn column in dataTable.Columns)
				{
					html += row[column.ColumnName] + " ";
				}
				html += "\n";
			}
			return html;
		}
	}
}