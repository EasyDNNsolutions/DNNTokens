using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Modules.DNNTokens.Common;
using DotNetNuke.Modules.DNNTokens.Components.Enums;
using DotNetNuke.Modules.DNNTokens.Components.Models;
using DotNetNuke.Modules.DNNTokens.Components.Sql;
using DotNetNuke.Modules.DNNTokens.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace DotNetNuke.Modules.DNNTokens.Components
{
	public class DNNTokens : IHttpModule
	{
		public string ModuleName => "DNNTokens";

		private Dictionary<int, List<TokenViewData>> tokensData = new Dictionary<int, List<TokenViewData>>();
		private Dictionary<int, GeneralPortalSettings> generalPortalSettingsList = new Dictionary<int, GeneralPortalSettings>();
		private Dictionary<int, List<int>> allowedTabsByPortal = new Dictionary<int, List<int>>();
		public void Init(HttpApplication context)
		{
			context.PreRequestHandlerExecute += new EventHandler(this.OnBeginRequest);
		}

		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			if (httpApplication == null)
				return;
			HttpContext context = httpApplication.Context;

			bool isCorrectContentType = context.CurrentHandler is CDefault || context.Response.ContentType == "application/json" || SupportedHandlers.HandlersList.Any(x => x.Equals(context.CurrentHandler.ToString()));
			if (context == null || !isCorrectContentType || context.Request.QueryString["popUp"] != null || context.Request.QueryString["ctl"] != null
				//check for the paths that we don't want to filter for 2xc edit mode
				|| context.Request.Url.AbsolutePath.Contains("tosic_sexycontent/dist/ng-edit")
				//check for the paths that we don't want to filter
				)
				return;

			HttpResponse response = httpApplication.Response;
			List<TokenViewData> tokenViewData = new List<TokenViewData>();
			if (!tokensData.ContainsKey(PortalSettings.Current.PortalId))
			{
				tokenViewData = InternalDataCache.GetAllPortalTokens(PortalSettings.Current.PortalId);
				tokensData.Add(PortalSettings.Current.PortalId, tokenViewData);
			}
			else
			{
				tokenViewData = tokensData[PortalSettings.Current.PortalId];
			}

			GeneralPortalSettings generalPortalSettings = null;
			if (!generalPortalSettingsList.ContainsKey(PortalSettings.Current.PortalId))
			{
				generalPortalSettings = InternalDataCache.GetGeneralPortalSettings(PortalSettings.Current.PortalId);
				if (generalPortalSettings == null)
				{
					generalPortalSettings = new GeneralPortalSettings()
					{
						PortalId = PortalSettings.Current.PortalId,
						RenderDNNTokens = false,
						ReplaceOnAllTabs = true
					};
				}
				generalPortalSettingsList.Add(PortalSettings.Current.PortalId, generalPortalSettings);
			}
			else
			{
				generalPortalSettings = generalPortalSettingsList[PortalSettings.Current.PortalId];
			}

			if (!generalPortalSettings.ReplaceOnAllTabs)
			{
				List<int> tabIdList = new List<int>();
				if (!allowedTabsByPortal.ContainsKey(PortalSettings.Current.PortalId))
				{
					tabIdList = InternalDataCache.GetAllowedTabsByPortal(PortalSettings.Current.PortalId);
					allowedTabsByPortal.Add(PortalSettings.Current.PortalId, tabIdList);
				}
				else
				{
					tabIdList = allowedTabsByPortal[PortalSettings.Current.PortalId];
				}
				if (!tabIdList.Any(t => t == PortalSettings.Current.ActiveTab.TabID))
					return;
			}

			UserInfo userInfo = (UserInfo)context.Items["UserInfo"];

			if (userInfo == null)
				userInfo = PortalSettings.Current.UserInfo;

			// Replace the response filter with our custom filter
			var newFilter = new StringReplaceFilter(response.Filter, tokenViewData, generalPortalSettings, userInfo, PortalSettings.Current);
			response.Filter = newFilter;
		}

		public void Dispose()
		{
			// Clean up resources if needed
		}

		/// <summary>
		/// The StringReplaceFilter class is a custom stream filter that performs string replacements
		/// on the response data before it is written to the response stream.
		/// </summary>
		class StringReplaceFilter : Stream
		{
			private readonly Stream _responseStream;
			private readonly StringBuilder _responseBuffer;
			public static List<TokenViewData> _tokens;
			public static GeneralPortalSettings _generalPortalSettings;
			public static PortalSettings _portalSettings;
			public static UserInfo _userInfo;
			public StringReplaceFilter(Stream responseStream, List<TokenViewData> tokens, GeneralPortalSettings generalPortalSettings, UserInfo userInfo, PortalSettings portalSettings)
			{
				_responseStream = responseStream;
				_responseBuffer = new StringBuilder();
				_tokens = tokens;
				_generalPortalSettings = generalPortalSettings;
				_portalSettings = portalSettings;
				_userInfo = userInfo;
			}

			public override bool CanRead => _responseStream.CanRead;
			public override bool CanSeek => _responseStream.CanSeek;
			public override bool CanWrite => _responseStream.CanWrite;
			public override long Length => _responseStream.Length;

			public override long Position
			{
				get => _responseStream.Position;
				set => _responseStream.Position = value;
			}

			public override void Flush()
			{
				// Perform the string replacement before flushing the stream
				string originalHtml = _responseBuffer.ToString();
				CultureInfo ciLanguage = Thread.CurrentThread.CurrentUICulture;
				MatchCollection regexMatxhes = Regex.Matches(originalHtml, Globals.RegexPattern);
				if (regexMatxhes != null && regexMatxhes.Count > 0 && _tokens != null && _tokens.Count > 0)
				{
					foreach (Match match in regexMatxhes)
					{
						var tokenMatch = match.Groups[1].Value;
						if (_tokens.Any(t => t.CategoryAndToken == tokenMatch))
						{
							originalHtml = ReplaceTokenInHtml(originalHtml, match, tokenMatch);
						}
					}
				}

				if (_generalPortalSettings.RenderDNNTokens && _portalSettings != null)
				{
					var portalSettingregexMatxhes = Regex.Matches(originalHtml, Globals.PortalSettingsRegexPattern);
					if (portalSettingregexMatxhes != null && portalSettingregexMatxhes.Count > 0)
					{
						foreach (Match match in portalSettingregexMatxhes)
						{
							string tokenMatch = match.Value;
							if (!string.IsNullOrEmpty(tokenMatch) && tokenMatch.Contains("[Portal:"))
							{
								tokenMatch = tokenMatch.Substring(tokenMatch.IndexOf("[Portal:") + "[Portal:".Length);
								tokenMatch = tokenMatch.TrimEnd(']');
								originalHtml = CoreTokenReplace.ReplacePortalToken(_portalSettings, originalHtml, match.Value, tokenMatch);
							}
						}
					}

					if (_portalSettings.ActiveTab != null)
					{
						var tabRegexMatxhes = Regex.Matches(originalHtml, Globals.TabRegexPattern);
						if (tabRegexMatxhes != null && tabRegexMatxhes.Count > 0)
						{
							foreach (Match match in tabRegexMatxhes)
							{
								string tokenMatch = match.Value;
								if (!string.IsNullOrEmpty(tokenMatch) && tokenMatch.Contains("[Tab:"))
								{
									tokenMatch = tokenMatch.Substring(tokenMatch.IndexOf("[Tab:") + "[Tab:".Length);
									tokenMatch = tokenMatch.TrimEnd(']');
									originalHtml = CoreTokenReplace.ReplaceTabToken(_portalSettings.ActiveTab, originalHtml, match.Value, tokenMatch);
								}
							}
						}
					}
				}

				if (_generalPortalSettings.RenderDNNTokens && _userInfo != null)
				{
					//Replace user tokens
					var userRegexMatxhes = Regex.Matches(originalHtml, Globals.UserRegexPattern);
					if (userRegexMatxhes != null && userRegexMatxhes.Count > 0)
					{
						foreach (Match match in userRegexMatxhes)
						{
							string tokenMatch = match.Value;
							if (!string.IsNullOrEmpty(tokenMatch) && tokenMatch.Contains("[User:"))
							{
								tokenMatch = tokenMatch.Substring(tokenMatch.IndexOf("[User:") + "[User:".Length);
								tokenMatch = tokenMatch.TrimEnd(']');
								originalHtml = CoreTokenReplace.ReplaceUserToken(_userInfo, originalHtml, match.Value, tokenMatch);
							}
						}
					}

					//Replace DateTime tokens

					var datetimeRegexMatxhes = Regex.Matches(originalHtml, Globals.DateTimeRegexPattern);
					if (datetimeRegexMatxhes != null && datetimeRegexMatxhes.Count > 0)
					{
						foreach (Match match in datetimeRegexMatxhes)
						{
							string tokenMatch = match.Value;
							if (!string.IsNullOrEmpty(tokenMatch) && tokenMatch.Contains("[DateTime:"))
							{
								tokenMatch = tokenMatch.Substring(tokenMatch.IndexOf("[DateTime:") + "[DateTime:".Length);
								tokenMatch = tokenMatch.TrimEnd(']');
								originalHtml = CoreTokenReplace.ReplaceDateTimeToken(_userInfo, originalHtml, match.Value, tokenMatch, ciLanguage);
							}
						}
					}
				}

				//replace culture tokens
				if (_generalPortalSettings.RenderDNNTokens)
				{
					var cultureRegexMatxhes = Regex.Matches(originalHtml, Globals.CultureRegexPattern);
					if (cultureRegexMatxhes != null && cultureRegexMatxhes.Count > 0)
					{
						foreach (Match match in cultureRegexMatxhes)
						{
							string tokenMatch = match.Value;
							if (!string.IsNullOrEmpty(tokenMatch) && tokenMatch.Contains("[Culture:"))
							{
								tokenMatch = tokenMatch.Substring(tokenMatch.IndexOf("[Culture:") + "[Culture:".Length);
								tokenMatch = tokenMatch.TrimEnd(']');
								originalHtml = CoreTokenReplace.ReplaceCultureToken(_userInfo, originalHtml, match.Value, tokenMatch, ciLanguage);
							}
						}
					}
				}


				//do another pass becouse of nested tokens
				if (regexMatxhes != null && regexMatxhes.Count > 0 && _tokens != null && _tokens.Count > 0)
				{
					regexMatxhes = Regex.Matches(originalHtml, Globals.RegexPattern);
					if (regexMatxhes != null && regexMatxhes.Count > 0 && _tokens != null && _tokens.Count > 0)
					{
						foreach (Match match in regexMatxhes)
						{
							var tokenMatch = match.Groups[1].Value;
							if (_tokens.Any(t => t.CategoryAndToken == tokenMatch))
							{
								originalHtml = ReplaceTokenInHtml(originalHtml, match, tokenMatch);
							}
						}
					}
				}

				// Convert the modified HTML back to bytes
				byte[] modifiedBytes = Encoding.UTF8.GetBytes(originalHtml);

				// Write the modified bytes to the response stream
				_responseStream.Write(modifiedBytes, 0, modifiedBytes.Length);
				_responseStream.Flush();
				_responseStream.Close();
				_responseBuffer.Clear();
			}

			public override void Close()
			{
				this._responseStream.Close();
			}
			public override int Read(byte[] buffer, int offset, int count)
			{
				return _responseStream.Read(buffer, offset, count);
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				return _responseStream.Seek(offset, origin);
			}

			public override void SetLength(long value)
			{
				_responseStream.SetLength(value);
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
				// Capture the response data in the buffer
				string chunk = Encoding.UTF8.GetString(buffer, offset, count);
				_responseBuffer.Append(chunk);
			}
			private static string ReplaceTokenInHtml(string originalHtml, Match match, string tokenMatch)
			{
				TokenViewData token = _tokens.Find(t => t.CategoryAndToken == tokenMatch);
				if (token.TokenType == TokenType.Text)
				{
					originalHtml = originalHtml.Replace(match.Value, token.TokenValue);
				}
				else if (token.TokenType == TokenType.Razor)
				{
					string html = RazorHelper.RenderRazorTemplate(token.TokenValue, match.Value);
					originalHtml = originalHtml.Replace(match.Value, html);
				}
				else if (token.TokenType == TokenType.SQLScript)
				{
					string errorMessage = string.Empty;
					DataTable dataTable = SQLExecute.GetSQLResults(token.TokenValue, ref errorMessage);
					if (errorMessage != string.Empty)
					{
						originalHtml = originalHtml.Replace(match.Value, errorMessage);
					}
					else
					{
						string html = SQLExecute.RenderSQLDataTableResults(dataTable);
						originalHtml = originalHtml.Replace(match.Value, html);
					}
				}
				return originalHtml;
			}
		}
	}
}