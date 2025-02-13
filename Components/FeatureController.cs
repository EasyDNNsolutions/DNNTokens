

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search.Entities;

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
					case "00.00.01":
						// run your custom code here
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