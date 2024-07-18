using DNNrocketAPI.Components;
using RocketPortal.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Text;
using RocketPluginProjectTemplate.Components;
using DNNrocketAPI.Interfaces;

namespace RocketPluginProjectTemplate.API
{
    public partial class StartConnect : IProcessCommand
    {
        private SimplisityInfo _postInfo;
        private SimplisityInfo _paramInfo;
        private RocketInterface _rocketInterface;
        private SessionParams _sessionParams;
        private DataObjectLimpet _dataObject;

        public Dictionary<string, object> ProcessCommand(string paramCmd, SimplisityInfo systemInfo, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
        {
            var strOut = ""; // return nothing if not matching commands.
            var storeParamCmd = paramCmd;

            paramCmd = InitCmd(paramCmd, systemInfo, interfaceInfo, postInfo, paramInfo, langRequired);

            var rtnDic = new Dictionary<string, object>();

            switch (paramCmd)
            {
                case "rocketpluginprojecttemplate_list":
                    strOut = RenderList();
                    break;
                case "rocketpluginprojecttemplate_detail":
                    strOut = GetDetail();
                    break;
                case "rocketpluginprojecttemplate_add":
                    strOut = Addarticle();
                    break;
                case "rocketpluginprojecttemplate_delete":
                    strOut = DeleteArticle();
                    break;
                case "rocketpluginprojecttemplate_save":
                    strOut = Savearticle();
                    break;
                case "rocketsystem_login":
                    strOut = ReloadPage();
                    break;
            }

            if (!rtnDic.ContainsKey("outputjson")) rtnDic.Add("outputhtml", strOut);
            return rtnDic;
        }

        private string ReloadPage()
        {
            UserUtils.SignOut();
            var razorTempl = _dataObject.AppThemeSystem.GetTemplate("Reload.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, _dataObject.PortalData, _dataObject.DataObjects, _dataObject.Settings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }

        public string InitCmd(string paramCmd, SimplisityInfo systemInfo, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
        {
            _postInfo = postInfo;
            _paramInfo = paramInfo;

            var portalid = _paramInfo.GetXmlPropertyInt("genxml/hidden/portalid");
            if (portalid == 0) portalid = PortalUtils.GetCurrentPortalId();

            _rocketInterface = new RocketInterface(interfaceInfo);
            _sessionParams = new SessionParams(_paramInfo);

            // Assign Langauge
            if (_sessionParams.CultureCode == "") _sessionParams.CultureCode = DNNrocketUtils.GetCurrentCulture();
            if (_sessionParams.CultureCodeEdit == "") _sessionParams.CultureCodeEdit = DNNrocketUtils.GetEditCulture();
            DNNrocketUtils.SetCurrentCulture(_sessionParams.CultureCode);
            DNNrocketUtils.SetEditCulture(_sessionParams.CultureCodeEdit);

            var systemkey = systemInfo.GetXmlProperty("genxml/systemkey");
            var basesystemkey = systemInfo.GetXmlProperty("genxml/basesystemkey");
            if (basesystemkey == "") basesystemkey = systemkey;

            _dataObject = new DataObjectLimpet(portalid, _sessionParams.ModuleRef, _sessionParams, systemkey);

            var securityData = new SecurityLimpet(portalid, basesystemkey, _rocketInterface, -1, -1, _dataObject.SystemKey);
            paramCmd = securityData.HasSecurityAccess(paramCmd, "rocketsystem_login");
            return paramCmd;
        }


    }
}
