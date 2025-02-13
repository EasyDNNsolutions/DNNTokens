using DotNetNuke.Entities.Tabs;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DotNetNuke.Modules.DNNTokens.Components
{
	/// <summary>
	/// A utility class for rendering portal tab lists.
	/// </summary>
	public class PortalTabListRender
	{
		/// <summary>
		/// Renders a list of portal tabs in a TreeView control.
		/// 
		/// This function retrieves a list of portal tabs from the TabController, 
		/// filters out the root tabs, and creates a TreeNode for each tab. 
		/// It then recursively adds child tabs to each TreeNode.
		/// 
		/// Parameters:
		/// treeView - The TreeView control to render the tabs in.
		/// portalId - The ID of the portal to retrieve tabs for.
		/// 
		/// Returns:
		/// None
		/// </summary>
		public static void RenderTabList(TreeView treeView, int portalId, List<int> selectedTabIdList)
		{
			List<TabInfo> portalTabs = TabController.GetPortalTabs(portalId, -1, false, string.Empty, true, false, true, true, true);

			List<TabInfo> rootPortalTabs = portalTabs.Where(a => a.ParentId == -1).OrderBy(a => a.TabOrder).ToList();
			foreach (TabInfo tabInfo in rootPortalTabs)
			{
				TreeNode tabNode = new TreeNode()
				{
					Text = tabInfo.TabName,
					Value = tabInfo.TabID.ToString(),
					Checked = selectedTabIdList.Any(a => a == tabInfo.TabID) ? true : false,
				};
				CreateChildTabNodes(tabNode, tabInfo.TabID, portalTabs, selectedTabIdList);

				treeView.Nodes.Add(tabNode);

			}
		}

		private static void CreateChildTabNodes(TreeNode treeNode, int parentTabID, List<TabInfo> tabInfos, List<int> selectedTabIdList)
		{
			List<TabInfo> childTabList = tabInfos.Where(a => a.ParentId == parentTabID).OrderBy(a => a.TabOrder).ToList();

			foreach (TabInfo tabInfo in childTabList)
			{
				TreeNode childTabNode = new TreeNode()
				{
					Text = tabInfo.TabName,
					Value = tabInfo.TabID.ToString(),
					Checked = selectedTabIdList.Any(a => a == tabInfo.TabID) ? true : false,
				};

				CreateChildTabNodes(childTabNode, tabInfo.TabID, tabInfos, selectedTabIdList);

				treeNode.ChildNodes.Add(childTabNode);
			}
		}
	}
}
