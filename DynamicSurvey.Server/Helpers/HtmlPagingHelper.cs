using DynamicSurvey.Server.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DynamicSurvey.Server.Helpers
{
	public static class HtmlPagingHelper
	{
		public static MvcHtmlString PageLinks(this HtmlHelper html, IPager pagingInfo, Func<int, string> pageUrl)
		{
			StringBuilder result = new StringBuilder();
			for (int i = 1; i <= pagingInfo.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
				tag.MergeAttribute("href", pageUrl(i));
				tag.InnerHtml = i.ToString();
				if (i == pagingInfo.CurrentPage)
					tag.AddCssClass("selected");
				result.Append(tag.ToString());
			}
			return MvcHtmlString.Create(result.ToString());
		}
	}
}