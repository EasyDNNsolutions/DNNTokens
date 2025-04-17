using System.Collections.Generic;

/// <summary>
/// Module Globals
/// </summary>
namespace DotNetNuke.Modules.DNNTokens.Common
{
	public static class SupportedHandlers
	{
		public static List<string> HandlersList = new List<string> {
		"EasyDNNSolutions.Modules.EasyDNNNews.ListContent.Services.ListContentHtml",
		"EasyDNNSolutions.Modules.EasyDNNNews.ListContent.Services.GetContentList",
		"EasyDNNSolutions.Modules.EasyDNNNews.ChameleonData",
		"EasyDNNSolutions.Modules.EasyDNNNews.GetGravityGaleryHtml",
		"EasyDNN.Modules.EasyDNNGallery.Services.loadmediahtmldata",
		"EasyDNN.Modules.EasyDNNGallery.ChameleonData",
		"EasyDNN.Modules.EasyDNNGallery.Services.portfolioproloadmediahtmldata",
		"EasyDNN.Modules.EasyDNNGallery.Services.PortfolioProLoadFullPageMedia"
		};
	}
}