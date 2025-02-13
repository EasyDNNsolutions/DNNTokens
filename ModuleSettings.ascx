<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="ModuleSettings.ascx.cs" Inherits="DotNetNuke.Modules.DNNTokens.Settings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnTokens">
	<div class="toast-container position-fixed top-0 end-0 p-3">
		<div class="toast align-items-center text-white bg-primary" role="alert" aria-live="assertive" aria-atomic="true" id="liveToastMessage">
			<div class="d-flex">
				<div class="toast-body">
					<asp:Label runat="server" ID="lblToastMessage"></asp:Label>
				</div>
				<button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
			</div>
		</div>
	</div>
	<div class="sidebar sidebar-fixed border-end bg-white">
		<div class="sidebar-header border-bottom">
			<div class="sidebar-brand text-center p-3">
				<svg class="sidebar-brand-full" width="200" height="45" alt="DNN Tokens Logo" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 498.84 115.9">
					<title>logosvg</title>
					<g id="Layer_2" data-name="Layer 2">
						<g id="Layer_1-2" data-name="Layer 1">
							<path d="M184.88,31.27V83.32h-5.13v-52H166.18V26.39h32.27v4.88Z" />
							<path d="M337.49,83.32V26.39h5.12V83.32Zm27.79,0L343.84,54.85l21.44-28.46h6.35L349.75,54.85l21.88,28.47Z" />
							<path d="M379.77,83.32V26.39h32v4.88H384.89V52.43h21.8v4.88h-21.8V78.43h26.92v4.89Z" />
							<path d="M450.91,83.32,427.17,41.87l-.47-2.46V83.32h-5.13V26.39h2.66L448,67.87l.51,2.43V26.39h5.12V83.32Z" />
							<path d="M498.84,70.14a12.88,12.88,0,0,1-1.17,5.44A14.68,14.68,0,0,1,490,83a19.41,19.41,0,0,1-6.5,1.09h-4.09a14,14,0,0,1-5.5-1.09,14.58,14.58,0,0,1-7.7-7.44A12.88,12.88,0,0,1,465,70.14V67.87l5.12-.83v3.1a8.33,8.33,0,0,0,.77,3.55A9.71,9.71,0,0,0,476,78.55a9.42,9.42,0,0,0,3.64.72h3.57a9.2,9.2,0,0,0,3.61-.72c1.14-.47,3.15-1.13,4-2a9.64,9.64,0,0,0,2.08-2.9,8.19,8.19,0,0,0,.78-3.55V68.43a9.5,9.5,0,0,0-1.15-4.88c-.77-1.3-2.79-2.41-4.06-3.32a19.88,19.88,0,0,0-4.31-2.32c-1.6-.64-3.25-1.27-4.94-1.91s-3.34-1.33-4.94-2.1A18,18,0,0,1,470,51,12.93,12.93,0,0,1,467,46.79a14.62,14.62,0,0,1-1.15-6.15V39.57a13.59,13.59,0,0,1,1.09-5.44,14,14,0,0,1,7.47-7.45,13.86,13.86,0,0,1,5.46-1.09h3.65a19.14,19.14,0,0,1,6.44,1.09,14.07,14.07,0,0,1,7.44,7.45,13.59,13.59,0,0,1,1.09,5.44V41l-5.12.83v-2.3a9,9,0,0,0-.71-3.56,9.35,9.35,0,0,0-1.95-2.87c-.82-.82-2.78-1.47-3.88-1.95a8.76,8.76,0,0,0-3.55-.71h-3.18a8.76,8.76,0,0,0-3.55.71,9.61,9.61,0,0,0-2.9,1.95,9,9,0,0,0-2,2.87,8.81,8.81,0,0,0-.72,3.56v1.07a8.65,8.65,0,0,0,1.15,4.6,10.82,10.82,0,0,0,3.06,3.2,21.72,21.72,0,0,0,4.31,2.3l4.94,2c1.69.67,4.34,1.41,5.94,2.22a18.48,18.48,0,0,1,4.31,3,13.27,13.27,0,0,1,3.06,4.31,14.91,14.91,0,0,1,1.15,6.19Z" />
							<path d="M263.5,115.9A58,58,0,0,1,241,4.55a57.95,57.95,0,0,1,63.53,94.37,57.8,57.8,0,0,1-41,17Zm0-111.71A53.76,53.76,0,1,0,317.26,58,53.82,53.82,0,0,0,263.5,4.19Z" />
							<path d="M225.24,45.41a2.21,2.21,0,0,1-.75-.14,2.08,2.08,0,0,1-1.2-2.7A43.05,43.05,0,0,1,293.53,27.1a2.1,2.1,0,1,1-2.93,3,38.63,38.63,0,0,0-27.1-11,39.11,39.11,0,0,0-36.3,25A2.09,2.09,0,0,1,225.24,45.41Z" />
							<path d="M263.87,100.93c-1.11,0-2.23,0-3.34-.12a43,43,0,0,1-34.15-21.64A2.09,2.09,0,1,1,230,77.1a38.84,38.84,0,0,0,30.82,19.53c1,.08,2,.11,3,.11a38.86,38.86,0,0,0,38.69-35.9,39.33,39.33,0,0,0,0-5.64,2.09,2.09,0,0,1,4.18-.28,43.34,43.34,0,0,1,0,6.24,43,43,0,0,1-42.85,39.77Z" />
							<path d="M47.31,43.58a5.69,5.69,0,0,0-1.79-1.22,5.44,5.44,0,0,0-2.18-.43H35.21V73.78h8.13a5.44,5.44,0,0,0,2.18-.44,6,6,0,0,0,1.8-1.2,5.71,5.71,0,0,0,1.2-1.77A5.44,5.44,0,0,0,49,68.19V47.54a5.36,5.36,0,0,0-.44-2.17A5.66,5.66,0,0,0,47.31,43.58Z" style="fill: #587bff; fill-rule: evenodd" />
							<path d="M118.69,22.31h-84C15,22.31,0,38.27,0,58H0C0,77.63,15,93.59,34.64,93.59h84.05A35.64,35.64,0,0,0,154.33,58h0A35.64,35.64,0,0,0,118.69,22.31ZM54.41,68.19a10.72,10.72,0,0,1-.85,4.23h0a11.11,11.11,0,0,1-2.33,3.45,10.87,10.87,0,0,1-3.47,2.33,10.65,10.65,0,0,1-4.25.85H29.76V36.65H43.51a10.68,10.68,0,0,1,4.25.85,10.84,10.84,0,0,1,5.8,5.8,10.81,10.81,0,0,1,.85,4.24ZM86,79.05H82.61L66.75,51.37V79.05H61.31V36.65h3.31L80.51,64.41V36.65H86Zm32.14,0h-3.35L98.9,51.37V79.05H93.45V36.65h3.31l15.89,27.76V36.65h5.45Z" style="fill: #587bff; fill-rule: evenodd" />
						</g>
					</g></svg>
			</div>
		</div>
		<nav class="navbar navbar-expand-lg p-4" id="navbarNavAltMarkup">
			<div class="navbar-nav d-grid gap-3 text-uppercase w-100">
				<asp:Button runat="server" ID="navAdEditToken" CssClass="nav-link active btn btn-outline-secondary dnnT_iconButton dnnT_add" Text="Add token" resourcekey="AddToken" OnClick="navAdEditToken_Click"></asp:Button>
				<asp:Button runat="server" ID="navTokensList" CssClass="nav-link btn btn-outline-secondary dnnT_iconButton dnnT_list" Text="Token list" resourcekey="TokenList" OnClick="navTokensList_Click"></asp:Button>
				<asp:Button runat="server" ID="navAdEditCategory" CssClass="nav-link btn btn-outline-secondary dnnT_iconButton dnnT_categories" Text="Categories" resourcekey="Categories" OnClick="navAdEditCategory_Click"></asp:Button>
				<asp:Button runat="server" ID="navGeneralPortalSettings" CssClass="nav-link btn btn-outline-secondary dnnT_iconButton dnnT_settings" Text="Settings" resourcekey="Settings" OnClick="navGeneralPortalSettings_Click"></asp:Button>
				<hr />
				<asp:Button runat="server" ID="btnClearCache" CssClass="nav-link btn btn-outline-secondary dnnT_iconButton dnnT_clearCache" Text="Clear Cache" resourcekey="ClearCache" OnClick="btnClearCache_Click"></asp:Button>
				<asp:HyperLink ID="hlCloseSettings" runat="server" resourcekey="Close" CssClass="nav-link btn btn-outline-danger dnnT_iconButton dnnt_close"  >Close</asp:HyperLink>
			</div>
		</nav>
	</div>
	<div class="wrapper d-flex flex-column min-vh-100 bg-light">
		<asp:Panel class="p-3 p-lg-5" runat="server" ID="divAddEditToken" DefaultButton="lbAddToken">
			<h2 class="mb-3"><%=LocalizeString("AddToken.Text")%></h2>
			<fieldset>
				<div class="mb-3">
					<label for="txtTokenName" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblName.Help")%>"><%=LocalizeString("lblName.Text")%></label>
					<asp:TextBox ID="txtTokenName" runat="server" class="form-control" MaxLength="250" />
					<asp:RequiredFieldValidator ID="rfvTokenName" class="invalid-feedback" runat="server" resourcekey="TokenName.Required" Display="Dynamic" ControlToValidate="txtTokenName" ValidationGroup="vgAddToken" />
				</div>
				<div class="mb-3">
					<label for="txtTokenValue" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblTokenValue.Help")%>"><%=LocalizeString("lblTokenValue.Text")%></label>
					<asp:TextBox ID="txtTokenValue" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="7" Columns="20" />
					<asp:RequiredFieldValidator ID="rfvDescription" CssClass="invalid-feedback" runat="server" resourcekey="TokenDecription.Required" Display="Dynamic" ControlToValidate="txtTokenValue" ValidationGroup="vgAddToken" />
				</div>
				<div class="mb-3">
					<label for="ddlCategorySelect" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblCategorySelect.Help")%>"><%=LocalizeString("lblCategorySelect.Text")%></label>
					<asp:DropDownList ID="ddlCategorySelect" runat="server" class="form-select" DataValueField="Id" DataTextField="Name" DataSourceID="odsCategoryListSelection" AppendDataBoundItems="True">
						<asp:ListItem Selected="True" Text="No category" Value="-1"></asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="mb-3">
					<label for="ddlTokenTypeSelect" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblTokenTypeSelect.Help")%>"><%=LocalizeString("lblTokenTypeSelect.Text")%></label>
					<asp:DropDownList ID="ddlTokenTypeSelect" runat="server" class="form-select">
						<asp:ListItem Selected="True" Text="Text" resourcekey="liTextHtml" Value="0"></asp:ListItem>
						<asp:ListItem Text="Razor" resourcekey="liRazor" Value="1"></asp:ListItem>
						<asp:ListItem Text="Razor" resourcekey="liSQLScript" Value="2"></asp:ListItem>
					</asp:DropDownList>
				</div>
				<div class="mb-3">
					<label for="rblScopeSelect" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("ScopeSelect.Help")%>"><%=LocalizeString("ScopeSelect.Text")%></label>
					<asp:RadioButtonList ID="rblScopeSelect" runat="server" CssClass="form-check" RepeatDirection="Horizontal" RepeatLayout="Flow">
						<asp:ListItem Value="0" class="form-check-input" resourcekey="liPortal" Text="Portal" Selected="True" />
						<asp:ListItem Value="1" class="form-check-input" resourcekey="liHost" Text="Host" />
					</asp:RadioButtonList>
				</div>
				<div class="mb-3">
					<label for="tbxTokenTestResults" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblTokenTestResults.Help")%>"><%=LocalizeString("lblTokenTestResults.Text")%></label>
					<asp:TextBox ID="tbxTokenTestResults" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="20" />
					<asp:Button ID="btnTestToken" runat="server" OnClick="btnTestToken_Click" resourcekey="btnTestToken" CssClass="btn btn-secondary mt-3" ValidationGroup="vgAddToken" />
				</div>
				<div class="mb-3">
					<asp:Label ID="lblAddTokenMessage" runat="server" CssClass="dnnFormMessage" EnableViewState="false" Visible="false"></asp:Label>
				</div>
				<hr class="my-4">
				<div class="mb-3">
					<asp:Button ID="lbAddToken" runat="server" OnClick="lbAddToken_Click" resourcekey="lbAddToken" CssClass="btn btn-primary dnnT_iconButton dnnT_add btn-lg" ValidationGroup="vgAddToken" />
					<asp:Button ID="btnUpdateToken" runat="server" OnClick="lbAddToken_Click" Visible="false" resourcekey="btnUpdateToken" CssClass="btn btn-primary btn-lg" ValidationGroup="vgAddToken" />
					<asp:Button ID="btnCancelUpdateToken" runat="server" OnClick="btnCancelUpdateToken_Click" Visible="false" resourcekey="btnCancelUpdateToken" CssClass="btn btn-warning btn-lg" ValidationGroup="vgAddToken" />
					<asp:HiddenField ID="hfTokenId" runat="server" />
				</div>
			</fieldset>
		</asp:Panel>

		<asp:Panel class="p-3 p-lg-5" runat="server" ID="divAddEditCategory" Visible="false" DefaultButton="btnAddCategory">
			<h2 class="mb-3"><%=LocalizeString("AddCategory.Text")%></h2>
			<fieldset>
				<div class="mb-3">
					<label cssclass="form-label" for="txtCategoryName" class="form-label" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblCategoryName.Help")%>"><%=LocalizeString("lblCategoryName.Text")%></label>
					<asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" MaxLength="250" />
					<asp:RequiredFieldValidator ID="rfvCategoryName" CssClass="invalid-feedback" runat="server" resourcekey="CategoryName.Required" Display="Dynamic" ControlToValidate="txtCategoryName" ValidationGroup="vgAddCategory" />
				</div>
				<div class="mb-3">
					<asp:Label ID="lblAddEditCategoryMessage" runat="server" CssClass="dnnFormMessage" EnableViewState="false" Visible="false"></asp:Label>
				</div>
				<div class="mb-3">
					<asp:Button ID="btnAddCategory" runat="server" resourcekey="btnAddCategory" OnClick="btnAddCategory_Click" CssClass="btn btn-primary dnnT_iconButton dnnT_add btn-lg" ValidationGroup="vgAddCategory" />
					<asp:HiddenField ID="hfCategoryEdit" runat="server" />
				</div>
				<div class="table-responsive" runat="server" id="divCategoriesList">
					<h2 class=" mt-3 mt-lg-5 mb-3"><%=LocalizeString("CategoryList.Text")%></h2>
					<asp:GridView ID="gvCategoryList" Width="100%" AutoGenerateColumns="false" runat="server" BorderStyle="None" GridLines="None" CssClass="table table-striped table-hover" ShowFooter="True" DataKeyNames="Id" DataSourceID="odsCategoryList" AllowPaging="True" PageSize="10" OnRowCommand="gvCategoryList_RowCommand" OnRowEditing="gvCategoryList_RowEditing" OnRowUpdated="gvCategoryList_RowUpdated">
						<HeaderStyle />
						<FooterStyle />
						<PagerStyle CssClass="pagination" />
						<Columns>
							<asp:TemplateField HeaderText="Action">
								<EditItemTemplate>
									<asp:LinkButton ID="lbCategoryUpdate" runat="server" CausesValidation="true" CommandName="Update" CssClass="btn btn-primary btn-sm" resourcekey="lbCategoryUpdate" CommandArgument='<%# Eval("Id") %>' />
									<asp:LinkButton ID="lbDocCancel" runat="server" CausesValidation="false" CommandName="Cancel" CssClass="btn btn-secondary btn-sm" resourcekey="lbCategoryCancel" />
								</EditItemTemplate>
								<ItemTemplate>
									<asp:Button ID="lblEditCategory" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="Edit" CssClass="btn btn-outline-primary btn-sm" Text="Edit"></asp:Button>
									<asp:Button ID="lbDeleteCategory" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteCategory" OnClientClick="return confirm('Do you really want to delete this category?');" CssClass="btn btn-outline-danger btn-sm" Text="Delete"></asp:Button>
								</ItemTemplate>
								<ItemStyle CssClass="text-center" Width="150" />
								<HeaderStyle CssClass="text-center" Width="150" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Id">
								<ItemTemplate>
									<asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("Id") %>' CssClass="form-control-plaintext" />
								</ItemTemplate>
								<ItemStyle />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Category name">
								<EditItemTemplate>
									<asp:TextBox ID="ttxtEditCategoryName" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control" MaxLength="250" />
								</EditItemTemplate>
								<ItemTemplate>
									<asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control-plaintext" />
								</ItemTemplate>
								<ItemStyle />
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
				</div>
			</fieldset>
		</asp:Panel>

		<div class="p-3 p-lg-5" runat="server" id="divTokenList" visible="false">
			<h2 class="mb-3"><%=LocalizeString("TokenList.Text")%></h2>
			<div class="table-responsive">
				<asp:GridView ID="gvTokenList" Width="100%" AutoGenerateColumns="false" runat="server" CssClass="table table-striped table-hover" ShowFooter="True" DataKeyNames="Id" DataSourceID="odsTokenList" AllowPaging="True" PageSize="10" OnRowCommand="gvTokenList_RowCommand">
					<HeaderStyle VerticalAlign="Top" />
					<FooterStyle />
					<PagerStyle CssClass="pagination" />
					<Columns>
						<asp:TemplateField HeaderText="Action">
							<ItemTemplate>
								<asp:Button ID="lblEditToken" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="EditToken" CssClass="btn btn-outline-primary btn-sm" Text="Edit"></asp:Button>
								<asp:Button ID="lbDeleteToken" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="DeleteToken" OnClientClick="return confirm('Do you really want to delete this token?');" CssClass="btn btn-outline-danger btn-sm" Text="Delete"></asp:Button>
							</ItemTemplate>
							<ItemStyle CssClass="text-center" Width="150" />
							<HeaderStyle CssClass="text-center" Width="150" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Id">
							<ItemTemplate>
								<asp:Label ID="lblTokenId" runat="server" Text='<%# Eval("Id") %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Token name">
							<ItemTemplate>
								<asp:Label ID="lblTokenName" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle CssClass="text-center bg-light" Width="150" />
							<HeaderStyle CssClass="text-center" Width="150" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Token value">
							<ItemTemplate>
								<asp:Label ID="lblTokenValue" runat="server" Text='<%#Server.HtmlEncode(Eval("TokenValue").ToString()) %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle />
							<HeaderStyle />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Category name">
							<ItemTemplate>
								<asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle CssClass="bg-light" Width="150" />
							<HeaderStyle Width="150" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Token">
							<ItemTemplate>
								<asp:Label ID="lblTokenUsage" runat="server" Text='<%#"[{"+ (String.IsNullOrEmpty(Eval("CategoryName") as string)?"": Eval("CategoryName")+":")+Eval("Name")+"}]"%>' CssClass="form-control-plaintext" />
								<%if (IsSecure)
									{ %>
								<button type="button" class="btn btn-outline-info btn-sm" onclick='<%# "copyToClipboard(\"[{" + (String.IsNullOrEmpty(Eval("CategoryName") as string)?"": Eval("CategoryName")+":")+Eval("Name") + "}]\");" %>'>Copy</button>
								<%} %>
							</ItemTemplate>
							<ItemStyle />
							<HeaderStyle CssClass="text-center" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Scope">
							<ItemTemplate>
								<asp:Label ID="lblTokenScope" runat="server" Text='<%# Eval("Scope") %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle CssClass="bg-light" />
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Type">
							<ItemTemplate>
								<asp:Label ID="lblTokenType" runat="server" Text='<%# Eval("TokenType") %>' CssClass="form-control-plaintext" />
							</ItemTemplate>
							<ItemStyle />
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				<div runat="server" id="divNoTokensMessage" visible="false" class="container py-5 mb-4 bg-light rounded-3">
					<div class="jumbotron jumbotron-fluid">
						<div class="container">
							<p class="lead"><%=LocalizeString("NoTokensAdded.Text")%></p>
						</div>
					</div>
				</div>
			</div>
		</div>

		<asp:Panel class="p-3 p-lg-5" runat="server" ID="pnlGeneralPortalSettings" Visible="false" DefaultButton="btnAddCategory">
			<h2 class="mb-3"><%=LocalizeString("GeneralPortalSettings.Text")%></h2>
			<fieldset>
				<div class="mb-3">
					<div>
						<asp:CheckBox ID="cbRenderDNNTokens" runat="server" AutoPostBack="False" />
						<label class="form-label" for="cbRenderDNNTokens" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("RenderDNNTokens.Help")%>"><%=LocalizeString("RenderDNNTokens.Text")%></label>
					</div>
				</div>
				<div class="mb-3">
					<div>
						<asp:CheckBox ID="cbReplaceOnAllTabs" runat="server" AutoPostBack="True" OnCheckedChanged="cbReplaceOnAllTabs_Click" />
						<label class="form-label" for="cbReplaceOnAllTabs" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("ReplaceOnAllTabs.Help")%>"><%=LocalizeString("ReplaceOnAllTabs.Text")%></label>
					</div>
				</div>
				<div class="mb-3" runat="server" id="divTabsList" visible="false">
					<label class="form-label" for="tvTabsList" data-bs-toggle="tooltip" data-bs-placement="top" title="<%=LocalizeString("lblTabsList.Help")%>"><%=LocalizeString("lblTabsList.Text")%></label>
					<div class="form-check">
						<asp:TreeView ID="tvTabsList" runat="server" ShowLines="True" ShowCheckBoxes="All" NodeWrap="True"></asp:TreeView>
					</div>
				</div>
				<div class="mb-3">
					<asp:Label ID="lblSaveGeneralPortalSettingsMessage" runat="server" CssClass="dnnFormMessage" EnableViewState="false" Visible="false"></asp:Label>
				</div>
				<div class="mb-3">
					<asp:Button ID="btnSaveGeneralPortalSettings" runat="server" resourcekey="btnSaveGeneralPortalSettings" OnClick="btnSaveGeneralPortalSettings_Click" CssClass="btn btn-primary btn-lg" ValidationGroup="vgGeneralPortalSettings" />
					<asp:HiddenField ID="HiddenField1" runat="server" />
				</div>
			</fieldset>
		</asp:Panel>
	</div>
	<script type="text/javascript">
		function showToast() {
			$(document).ready(function () {
				const toastLiveExample = document.getElementById('liveToastMessage')
				const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
				toastBootstrap.hide();
				toastBootstrap.show();
			});
		}

		function initTooltips() {
			var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
			var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
				return new bootstrap.Tooltip(tooltipTriggerEl)
			})
		}

		function copyToClipboard(copyText) {
			navigator.clipboard.writeText(copyText)
				.then(() => {
					$('#<%=lblToastMessage.ClientID %>').text('Copied to clipboard.');
					showToast();
				})
				.catch((error) => { console.log('Copy failed. Error: ${error}') })
		}

		initTooltips();
		function pageLoad(sender, args) {
			if (args.get_isPartialLoad()) {
				initTooltips();
			}
		}

	</script>

	<asp:ObjectDataSource ID="odsTokenList" TypeName="DotNetNuke.Modules.DNNTokens.DataProvider.DbContext.TokenDb" runat="server" EnablePaging="True" SelectMethod="GetPortalTokens" SelectCountMethod="GetTotalNumberOfTokensByPortalId">
		<SelectParameters>
			<asp:Parameter Name="portalId" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="10" />
			<asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="1" />
		</SelectParameters>
	</asp:ObjectDataSource>

	<asp:ObjectDataSource ID="odsCategoryList" TypeName="DotNetNuke.Modules.DNNTokens.DataProvider.DbContext.CategoryDb" runat="server" EnablePaging="True" SelectMethod="GetPortalCategories" SelectCountMethod="GetTotalNumberOfCategoriesByPortalId" UpdateMethod="UpdateCategory">
		<SelectParameters>
			<asp:Parameter Name="portalId" Type="Int32" />
			<asp:Parameter Name="maximumRows" Type="Int32" DefaultValue="10" />
			<asp:Parameter Name="startRowIndex" Type="Int32" DefaultValue="1" />
		</SelectParameters>
		<UpdateParameters>
			<asp:Parameter Name="ID" Type="Int32" />
			<asp:Parameter Name="Name" Type="String" />
		</UpdateParameters>
	</asp:ObjectDataSource>

	<asp:ObjectDataSource ID="odsCategoryListSelection" TypeName="DotNetNuke.Modules.DNNTokens.DataProvider.DbContext.CategoryDb" runat="server" SelectMethod="GetAllPortalCategories">
		<SelectParameters>
			<asp:Parameter Name="portalId" Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
