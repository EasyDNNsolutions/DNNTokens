using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.Client.ClientResourceManagement;
using System;

namespace DotNetNuke.Modules.DNNTokens
{
	public partial class View : PortalModuleBase, IActionable
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (UserInfo.IsSuperUser || UserInfo.IsInRole(PortalSettings.AdministratorRoleName))
				{
					hlModuleSettings.NavigateUrl = EditUrl("ModuleSettings") + "?SkinSrc=[G]Skins%2f_default%2fNo+Skin&ContainerSrc=[G]Containers%2f_default%2fNo+Container";
					ClientResourceManager.RegisterStyleSheet(this.Page, ControlPath + "Assets/admin.css");
				}
				else
					hlModuleSettings.Visible = false;

			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		public ModuleActionCollection ModuleActions
		{
			get
			{
				var actions = new ModuleActionCollection
					{
					{
						GetNextActionID(), Localization.GetString("ModuleSettings", LocalResourceFile),
							ModuleActionType.AddContent, "", "settings.gif", EditUrl("ModuleSettings")+"?SkinSrc=[G]Skins%2f_default%2fNo+Skin&ContainerSrc=[G]Containers%2f_default%2fNo+Container", false,
							SecurityAccessLevel.Admin, true, false
						}
					};
				return actions;
			}
		}
	}
}