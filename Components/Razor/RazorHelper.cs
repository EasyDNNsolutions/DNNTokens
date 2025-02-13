using RazorEngine;
using RazorEngine.Templating;
using System;

namespace DotNetNuke.Modules.DNNTokens.Components
{
	public class RazorHelper
	{
		public static string RenderRazorTemplate(string template, string templateKey)
		{
			try
			{
				var result = Engine.Razor.RunCompile(template, templateKey);
				return result;
			}
			catch (Exception exception)
			{
				var message = exception.Message;
				return message;
			}
		}
	}
}
