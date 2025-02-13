using DotNetNuke.Entities.Modules;

namespace DotNetNuke.Modules.DNNTokens.Components
{

	public class FeatureController : IUpgradeable
	{
		public string UpgradeModule(string version)
		{
			try
			{
				switch (version)
				{
					case "01.00.0":
						return "success";
					default:
						return "success";
				}
			}
			catch
			{
				return "failure";
			}
		}

	}
}