using DNNrocketAPI.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using RocketPluginProjectTemplate.Components;

namespace RocketPluginProjectTemplate.API
{
    public partial class StartConnect
    {
        private ArticleLimpet GetActiveArticle(int articleId, string cultureCode)
        {
            return new ArticleLimpet(_portalData.PortalId, articleId, cultureCode);
        }
        private ArticleLimpet GetActiveArticle(string cultureCode)
        {
            var articleId = _paramInfo.GetXmlPropertyInt("genxml/hidden/articleid");
            return new ArticleLimpet(_portalData.PortalId, articleId, cultureCode);
        }

        public String GetArticleDetail(int articleId, string cultureCode)
        {
            var articleData = GetActiveArticle(articleId, cultureCode);
            return GetArticleDetail(articleData);
        }
        public String GetArticleDetail(ArticleLimpet articleData)
        {
            var razorTempl = _appThemePlugin.GetTemplate("detail.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, articleData, _dataObjects, _passSettings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }
        public String GetDetail(string cultureCode)
        {
            var articleData = GetActiveArticle(cultureCode);
            return GetArticleDetail(articleData);
        }
        public String AddArticle(string cultureCode)
        {
            return GetArticleDetail(-1, cultureCode);
        }
        public String SaveArticle(string cultureCode)
        {
            var articleData = GetActiveArticle(cultureCode);
            articleData.Save(_postInfo);
            return GetArticleDetail(articleData);
        }
        private string RenderList()
        {
            var dataList = new ArticleLimpetList(_paramInfo, _portalData, _sessionParams.CultureCodeEdit, true);
            _dataObjects.Add("datalist", dataList);

            _sessionParams.RowCount = dataList.RecordCount; //Add row count for paging.

            var razorTempl = _appThemePlugin.GetTemplate("list.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, _portalData, _dataObjects, _passSettings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }


    }
}

