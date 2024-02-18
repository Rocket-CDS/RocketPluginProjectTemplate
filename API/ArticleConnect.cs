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
        private ArticleLimpet GetActivepArticle(int particleId, string cultureCode)
        {
            return new ArticleLimpet(_dataObject.PortalData.PortalId, particleId, cultureCode);
        }
        private ArticleLimpet GetActiveArticle(string cultureCode)
        {
            var particleId = _paramInfo.GetXmlPropertyInt("genxml/hidden/itemid");
            return new ArticleLimpet(_dataObject.PortalData.PortalId, particleId, cultureCode);
        }

        public String GetArticleDetail(int particleId, string cultureCode)
        {
            var particleData = GetActivepArticle(particleId, cultureCode);
            return GetpArticleDetail(particleData);
        }
        public String GetpArticleDetail(ArticleLimpet particleData)
        {
            var razorTempl = _dataObject.AppThemePlugin.GetTemplate("detail.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, particleData, _dataObject.DataObjects, _dataObject.Settings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }
        public String GetDetail()
        {
            var particleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            return GetpArticleDetail(particleData);
        }
        public String DeleteArticle()
        {
            var particleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            particleData.Delete();
            return RenderList();
        }
        public String AddpArticle()
        {
            return GetArticleDetail(-1, _sessionParams.CultureCodeEdit);
        }
        public String SavepArticle()
        {
            var particleData = GetActiveArticle(_sessionParams.CultureCodeEdit);
            particleData.Save(_postInfo);
            return GetpArticleDetail(particleData);
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

