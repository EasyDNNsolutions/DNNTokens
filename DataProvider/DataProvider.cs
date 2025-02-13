using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.DNNTokens.DataProvider
{
	internal static class SqlDataProvider
	{
		private static readonly string TablePrefix = "DNNTokens_";

		private static string _databaseOwner;
		private static string _objectQualifier;

		internal static SqlConnection GetSqlConnection()
		{
			SqlConnection connection = new SqlConnection(Config.GetConnectionString());
			connection.Open();
			return connection;
		}

		internal static string DnnTableNameWrapper(this string tableName)
		{
			return " " + GetOwnerAndQualifier() + tableName + "] ";
		}

		internal static string ModuleTableNameWrapper(this string tableName)
		{
			return " " + GetOwnerAndQualifier() + TablePrefix + tableName + "] ";
		}

		public static string GetObjectQualifier()
		{
			if (_objectQualifier == null)
			{
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string objectQualifierTemp = provider.Attributes["objectQualifier"];
				if (!string.IsNullOrEmpty(objectQualifierTemp) && !objectQualifierTemp.EndsWith("_"))
					objectQualifierTemp += "_";

				_objectQualifier = objectQualifierTemp;
			}

			return _objectQualifier;
		}

		private static string GetDatabaseOwner()
		{
			if (_databaseOwner == null)
			{
				ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration("data");
				Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];

				string databaseOwnerTemp = provider.Attributes["databaseOwner"];
				if (!string.IsNullOrEmpty(databaseOwnerTemp) && !databaseOwnerTemp.EndsWith("."))
					databaseOwnerTemp += ".";

				_databaseOwner = databaseOwnerTemp;

				if (_objectQualifier == null)
				{
					string objectQualifierTemp = provider.Attributes["objectQualifier"];
					if (!string.IsNullOrEmpty(objectQualifierTemp) && !objectQualifierTemp.EndsWith("_"))
						objectQualifierTemp += "_";

					_objectQualifier = objectQualifierTemp;
				}
			}

			return _databaseOwner;
		}

		private static string GetOwnerAndQualifier()
		{
			return GetDatabaseOwner() + "[" + GetObjectQualifier();
		}
	}
}