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
        private ArticleLimpet GetActivearticle(int articleId, string cultureCode)
        {
            return new ArticleLimpet(_dataObject.PortalData.PortalId, articleId, cultureCode);
        }
        private ArticleLimpet GetActiveArticle(string cultureCode)
        {
            var articleId = _paramInfo.GetXmlPropertyInt("genxml/hidden/itemid");
            return new ArticleLimpet(_dataObject.PortalData.PortalId, articleId, cultureCode);
        }

        public String GetArticleDetail(int articleId, string cultureCode)
        {
            var articleData = GetActivearticle(articleId, cultureCode);
            return GetarticleDetail(articleData);
        }
        public String GetarticleDetail(ArticleLimpet articleData)
        {
            var razorTempl = _dataObject.AppThemePlugin.GetTemplate("detail.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, articleData, _dataObject.DataObjects, _dataObject.Settings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }
        public String GetDetail()
        {
            var articleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            return GetarticleDetail(articleData);
        }
        public String DeleteArticle()
        {
            var articleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            articleData.Delete();
            return RenderList();
        }
        public String Addarticle()
        {
            return GetArticleDetail(-1, _sessionParams.CultureCodeEdit);
        }
        public String Savearticle()
        {
            var articleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            articleData.Save(_postInfo);
            return GetarticleDetail(articleData);
        }
        private string RenderList()
        {
            var dataList = new ArticleLimpetList(_paramInfo, _dataObject.PortalData, _sessionParams.CultureCodeEdit, true);
            _dataObject.SetDataObject("datalist", dataList);

            _sessionParams.RowCount = dataList.RecordCount; //Add row count for paging.

            var razorTempl = _dataObject.AppThemePlugin.GetTemplate("list.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, _dataObject.PortalData, _dataObject.DataObjects, _dataObject.Settings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }


    }
}

