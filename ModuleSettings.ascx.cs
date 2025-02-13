using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Modules.DNNTokens.Components;
using DotNetNuke.Modules.DNNTokens.Components.Enums;
using DotNetNuke.Modules.DNNTokens.Components.Models;
using DotNetNuke.Modules.DNNTokens.Components.Sql;
using DotNetNuke.Modules.DNNTokens.DataProvider.DbContext;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.UI.WebControls.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNetNuke.Modules.DNNTokens
{
	public partial class Settings : PortalModuleBase
	{
		protected bool IsSecure { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				DotNetNuke.Framework.AJAX.RegisterScriptManager();
				ScriptManager Sc = (ScriptManager)DotNetNuke.Framework.AJAX.GetScriptManager(this.Page);

				//Test if we are on https
				IsSecure = Request.IsSecureConnection;

				if (!Page.IsPostBack)
				{
					hlCloseSettings .NavigateUrl = DotNetNuke.Common.Globals.NavigateURL();
					odsTokenList.SelectParameters["portalId"].DefaultValue = PortalId.ToString();
					odsCategoryList.SelectParameters["portalId"].DefaultValue = PortalId.ToString();
					odsCategoryListSelection.SelectParameters["portalId"].DefaultValue = PortalId.ToString();

					//GeneralPortalSettings
					GeneralPortalSettings generalPortalSettings = GeneralPortalSettingsDb.GetGeneralPortalSettings(PortalId);
					if (generalPortalSettings == null)
					{
						generalPortalSettings = new GeneralPortalSettings()
						{
							PortalId = PortalId,
							RenderDNNTokens = false,
							ReplaceOnAllTabs = true
						};
					}
					List<int> tabIdList = new List<int>();
					if (!generalPortalSettings.ReplaceOnAllTabs)
					{
						tabIdList = PortalTabsDb.GetAllPortalTabs(PortalId);
					}

					PortalTabListRender.RenderTabList(tvTabsList, PortalId, tabIdList);

					cbRenderDNNTokens.Checked = generalPortalSettings.RenderDNNTokens;
					cbReplaceOnAllTabs.Checked = generalPortalSettings.ReplaceOnAllTabs;

					cbReplaceOnAllTabs_Click(null, null);

				}
				if (gvTokenList.Rows.Count == 0)
				{
					divNoTokensMessage.Visible = true;
				}
				divCategoriesList.Visible = gvTokenList.Rows.Count > 0;
				ClientResourceManager.RegisterStyleSheet(this.Page, ControlPath + "Assets/bootstrap.min.css");
				ClientResourceManager.RegisterStyleSheet(this.Page, ControlPath + "Assets/dnnTokensStyle.css");
				ClientResourceManager.RegisterScript(this.Page, ControlPath + "Assets/bootstrap.bundle.min.js");
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

		protected void navAdEditToken_Click(object sender, EventArgs e)
		{
			SetActiveView(divAddEditToken, navAdEditToken);
		}

		protected void navTokensList_Click(object sender, EventArgs e)
		{
			SetActiveView(divTokenList, navTokensList);
		}
		protected void navGeneralPortalSettings_Click(object sender, EventArgs e)
		{
			SetActiveView(pnlGeneralPortalSettings, navGeneralPortalSettings);
		}
		protected void navAdEditCategory_Click(object sender, EventArgs e)
		{
			SetActiveView(divAddEditCategory, navAdEditCategory);
		}

		private void SetActiveView(Control activeDiv, WebControl activeNav)
		{
			divAddEditToken.Visible = false;
			divTokenList.Visible = false;
			divAddEditCategory.Visible = false;
			pnlGeneralPortalSettings.Visible = false;
			navAdEditToken.RemoveCssClass("active");
			navTokensList.RemoveCssClass("active");
			navAdEditCategory.RemoveCssClass("active");
			navGeneralPortalSettings.RemoveCssClass("active");

			activeDiv.Visible = true;
			activeNav.AddCssClass("active");
		}

		protected void cbReplaceOnAllTabs_Click(object sender, EventArgs e)
		{
			if (cbReplaceOnAllTabs.Checked == true)
			{
				divTabsList.Visible = false;
			}
			else
			{
				divTabsList.Visible = true;
			}
		}

		protected void gvTokenList_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
		{
			if (e.CommandName == "DeleteToken")
			{
				int tokenId = Convert.ToInt32(e.CommandArgument);
				TokenDb.DeleteToken(tokenId);
				gvTokenList.DataBind();

				lblToastMessage.Text = "Token deleted";
				//Execute Javascript function on postback
				ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);

			}
			else if (e.CommandName == "EditToken")
			{
				int tokenId = Convert.ToInt32(e.CommandArgument);
				Token token = TokenDb.GetTokenById(tokenId);

				//Fill in fields with token data
				txtTokenName.Text = token.Name;
				txtTokenValue.Text = token.TokenValue;
				rblScopeSelect.SelectedValue = ((int)token.Scope).ToString();
				ddlTokenTypeSelect.SelectedValue = ((int)token.TokenType).ToString();
				hfTokenId.Value = tokenId.ToString();
				if (token.CategoryId.HasValue)
				{
					if (ddlCategorySelect.Items.FindByValue(token.CategoryId.Value.ToString()) != null)
						ddlCategorySelect.SelectedValue = token.CategoryId.Value.ToString();
				}
				SetActiveView(divAddEditToken, navAdEditToken);

				lbAddToken.Visible = false;
				btnUpdateToken.Visible = true;
				btnCancelUpdateToken.Visible = true;
			}
		}

		protected void btnAddCategory_Click(object sender, EventArgs e)
		{
			////Check if name is not empty
			if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
			{
				lblAddEditCategoryMessage.Visible = true;
				lblAddEditCategoryMessage.CssClass += " alert alert-danger";
				lblAddEditCategoryMessage.Text = "Category name is required";
				return;
			}

			////Check if category name already exists
			if (CategoryDb.GetCategoryByName(PortalId, txtCategoryName.Text.Trim()) != null)
			{
				lblAddEditCategoryMessage.Visible = true;
				lblAddEditCategoryMessage.CssClass += " alert alert-danger";
				lblAddEditCategoryMessage.Text = "Category name already exists";
				return;
			}

			Category category = new Category
			{
				Name = txtCategoryName.Text.Trim(),
				PortalId = PortalId,
			};
			CategoryDb.SaveCategory(category);

			////Clear textboxes
			txtCategoryName.Text = string.Empty;

			////Show success
			lblToastMessage.Text = Localization.GetString("CategoryAdded", LocalResourceFile);

			////Refresh category list
			gvCategoryList.DataBind();
			ddlCategorySelect.DataBind();
			ddlCategorySelect.Items.Clear();
			ddlCategorySelect.Items.Add(new ListItem("No category", "-1"));
			ddlCategorySelect.DataBind();
			ddlCategorySelect.SelectedIndex = 0;
			//Execute Javascript function on postback
			ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
		}

		protected void gvCategoryList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "DeleteCategory")
			{
				int categroryId = Convert.ToInt32(e.CommandArgument);

				if (CategoryDb.CategoryInUse(categroryId))
				{
					lblToastMessage.Text = "Category is in use and cannot be deleted";
					//Execute Javascript function on postback
					ScriptManager.RegisterStartupScript(gvCategoryList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
					return;
				}

				CategoryDb.DeleteCategory(categroryId);
				gvTokenList.DataBind();
				gvCategoryList.DataBind();

				lblToastMessage.Text = "Category deleted";
				//Execute Javascript function on postback
				ScriptManager.RegisterStartupScript(gvCategoryList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);

			}
		}

		protected void gvCategoryList_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvCategoryList.EditIndex = e.NewEditIndex;
			//gvCategoryList.DataBind();
		}

		protected void gvCategoryList_RowUpdated(object sender, GridViewUpdatedEventArgs e)
		{
			gvCategoryList.EditIndex = -1;
			gvTokenList.DataBind();
		}

		protected void btnCancelUpdateToken_Click(object sender, EventArgs e)
		{
			////Clear textboxes
			txtTokenName.Text = string.Empty;
			txtTokenValue.Text = string.Empty;
			ddlCategorySelect.SelectedIndex = -1;
			hfTokenId.Value = string.Empty;

			lbAddToken.Visible = true;
			btnUpdateToken.Visible = false;
			btnCancelUpdateToken.Visible = false;

			////Show success
			//lblAddTokenMessage.Visible = true;
			//lblAddTokenMessage.CssClass += " alert alert-success";
			lblToastMessage.Text = Localization.GetString("TokenEditCanceled", LocalResourceFile);

			////Refresh token list
			gvTokenList.DataBind();
			//Execute Javascript function on postback
			ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
		}

		protected void lbAddToken_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			////Check if name is not empty
			if (string.IsNullOrEmpty(txtTokenName.Text.Trim()))
			{
				lblAddTokenMessage.Visible = true;
				lblAddTokenMessage.CssClass += " alert alert-danger";
				lblAddTokenMessage.Text = "Token name is required";
				return;
			}

			////check if value is not empty
			if (string.IsNullOrEmpty(txtTokenValue.Text.Trim()))
			{
				lblAddTokenMessage.Visible = true;
				lblAddTokenMessage.Text = "Token value is required";
				return;
			}

			if (btn.ID != "btnUpdateToken")
			{
				////Check if token name already exists
				if (TokenDb.GetTokenByName(PortalId, txtTokenName.Text.Trim()) != null)
				{
					lblAddTokenMessage.Visible = true;
					lblAddTokenMessage.CssClass += " alert alert-danger";
					lblAddTokenMessage.Text = "Token name already exists";
					return;
				}
			}
			if (ddlTokenTypeSelect.SelectedValue == "2")
			{
				string tokenValue = txtTokenValue.Text.Trim();
				if (tokenValue.ToLowerInvariant().Contains("delete") || tokenValue.ToLowerInvariant().Contains("update") || tokenValue.ToLowerInvariant().Contains("drop"))
				{
					lblAddTokenMessage.Visible = true;
					lblAddTokenMessage.CssClass += " alert alert-danger";
					lblAddTokenMessage.Text = Localization.GetString("InvalidSQLquery", LocalResourceFile);
					//lblToastMessage.Text = Localization.GetString("InvalidSQLquery", LocalResourceFile);
					//ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
					return;
				}
			}

			Token token = new Token
			{
				Name = txtTokenName.Text.Trim(),
				PortalId = PortalId,
				TokenValue = txtTokenValue.Text,
				TokenType = (TokenType)int.Parse(ddlTokenTypeSelect.SelectedValue),
				Scope = (TokenScope)Enum.Parse(typeof(TokenScope), rblScopeSelect.SelectedValue),
				UserId = UserId,
			};

			if (btn.ID == "btnUpdateToken")
			{
				token.Id = int.Parse(hfTokenId.Value);
			}

			if (ddlCategorySelect.SelectedIndex != -1 && ddlCategorySelect.SelectedValue != "-1")
			{
				token.CategoryId = int.Parse(ddlCategorySelect.SelectedValue);
			}

			TokenDb.InsertUpdate(token);

			////Clear textboxes
			txtTokenName.Text = string.Empty;
			txtTokenValue.Text = string.Empty;
			ddlCategorySelect.SelectedIndex = -1;
			ddlTokenTypeSelect.SelectedValue = "0";
			hfTokenId.Value = string.Empty;

			////Show success;
			if (btn.ID == "btnUpdateToken")
			{
				lblToastMessage.Text = Localization.GetString("TokenUpdated", LocalResourceFile);

				lbAddToken.Visible = true;
				btnUpdateToken.Visible = false;
				btnCancelUpdateToken.Visible = false;
			}
			else
			{
				lblToastMessage.Text = Localization.GetString("TokenAdded", LocalResourceFile);
			}

			////Refresh token list
			gvTokenList.DataBind();
			//Execute Javascript function on postback
			ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
		}

		protected void btnSaveGeneralPortalSettings_Click(object sender, EventArgs e)
		{
			//check if at least one tab is selected
			if (!cbReplaceOnAllTabs.Checked)
			{
				if (tvTabsList.CheckedNodes.Count == 0)
				{
					lblToastMessage.Text = Localization.GetString("NoTabsSelected", LocalResourceFile);
					//Execute Javascript function on postback
					ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
					return;
				}
			}

			//Save GeneralPortalSettings
			GeneralPortalSettings generalPortalSettings = new GeneralPortalSettings
			{
				PortalId = PortalId,
				RenderDNNTokens = cbRenderDNNTokens.Checked,
				ReplaceOnAllTabs = cbReplaceOnAllTabs.Checked
			};
			GeneralPortalSettingsDb.SaveGeneralPortalSettings(generalPortalSettings);


			if (!cbReplaceOnAllTabs.Checked)
			{
				//Delete all portal tabs
				PortalTabsDb.DeleteAllPortalTabs(PortalId);

				//Save checked tabs
				foreach (TreeNode rootNode in tvTabsList.Nodes)
					SaveCheckedNode(rootNode);
			}
			//Show success
			lblToastMessage.Text = Localization.GetString("GeneralPortalSettingsSaved", LocalResourceFile);

			//Execute Javascript function on postback
			ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);

		}

		private void SaveCheckedNode(TreeNode node)
		{
			if (node.Checked)
			{
				PortalTabsDb.SavePortalTab(PortalId, Convert.ToInt32(node.Value));
			}

			foreach (TreeNode child in node.ChildNodes)
				SaveCheckedNode(child);
		}

		protected void btnTestToken_Click(object sender, EventArgs e)
		{
			if (ddlTokenTypeSelect.SelectedValue == "0")
			{
				tbxTokenTestResults.Text = txtTokenValue.Text;
				//ltTestTokenResults.Text = txtTokenValue.Text;
			}
			else if (ddlTokenTypeSelect.SelectedValue == "1")
			{
				string html = RazorHelper.RenderRazorTemplate(txtTokenValue.Text, Guid.NewGuid().ToString());
				tbxTokenTestResults.Text = html;
			}
			else if (ddlTokenTypeSelect.SelectedValue == "2")
			{
				string tokenValue = txtTokenValue.Text.Trim();
				if (tokenValue.ToLowerInvariant().Contains("delete") || tokenValue.ToLowerInvariant().Contains("update") || tokenValue.ToLowerInvariant().Contains("drop"))
				{
					lblToastMessage.Text = Localization.GetString("InvalidSQLquery", LocalResourceFile);
					ScriptManager.RegisterStartupScript(gvTokenList, gvTokenList.GetType(), "DNNTokensShowToast", "showToast();", true);
					return;
				}

				string errorMessage = string.Empty;
				DataTable dataTable = SQLExecute.GetSQLResults(txtTokenValue.Text, ref errorMessage);
				if (errorMessage != string.Empty)
				{
					tbxTokenTestResults.Text = errorMessage;
					return;
				}

				tbxTokenTestResults.Text = SQLExecute.RenderSQLDataTableResults(dataTable);
				//ltTestTokenResults.Text = html;
			}
		}

        protected void btnClearCache_Click(object sender, EventArgs e)
        {
			DataCache.ClearCache("dnntokens_" + PortalId);
			Config.Touch();
		}
    }
}