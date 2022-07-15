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
        private pArticleLimpet GetActivepArticle(int particleId, string cultureCode)
        {
            return new pArticleLimpet(_portalData.PortalId, particleId, cultureCode);
        }
        private pArticleLimpet GetActivepArticle(string cultureCode)
        {
            var particleId = _paramInfo.GetXmlPropertyInt("genxml/hidden/particleid");
            return new pArticleLimpet(_portalData.PortalId, particleId, cultureCode);
        }

        public String GetpArticleDetail(int particleId, string cultureCode)
        {
            var particleData = GetActivepArticle(particleId, cultureCode);
            return GetpArticleDetail(particleData);
        }
        public String GetpArticleDetail(pArticleLimpet particleData)
        {
            var razorTempl = _appThemePlugin.GetTemplate("detail.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, particleData, _dataObjects, _passSettings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }
        public String GetDetail(string cultureCode)
        {
            var particleData = GetActivepArticle(cultureCode);
            return GetpArticleDetail(particleData);
        }
        public String AddpArticle(string cultureCode)
        {
            return GetpArticleDetail(-1, cultureCode);
        }
        public String SavepArticle(string cultureCode)
        {
            var particleData = GetActivepArticle(cultureCode);
            particleData.Save(_postInfo);
            return GetpArticleDetail(particleData);
        }
        private string RenderList()
        {
            var dataList = new pArticleLimpetList(_paramInfo, _portalData, _sessionParams.CultureCodeEdit, true);
            _dataObjects.Add("datalist", dataList);

            _sessionParams.RowCount = dataList.RecordCount; //Add row count for paging.

            var razorTempl = _appThemePlugin.GetTemplate("list.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, _portalData, _dataObjects, _passSettings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }


    }
}

