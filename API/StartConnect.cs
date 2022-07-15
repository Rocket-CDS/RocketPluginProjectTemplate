using DNNrocketAPI.Components;
using RocketPortal.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Text;
using RocketPluginProjectTemplate.Components;

namespace RocketPluginProjectTemplate.API
{
    public partial class StartConnect : DNNrocketAPI.APInterface
    {
        private SimplisityInfo _postInfo;
        private SimplisityInfo _paramInfo;
        private RocketInterface _rocketInterface;
        private Dictionary<string, string> _passSettings;
        private SystemLimpet _systemData;
        private SessionParams _sessionParams;
        private AppThemeSystemLimpet _appThemeSystem;
        private AppThemeSystemLimpet _appThemePlugin;
        private AppThemeDNNrocketLimpet _appThemePortal;
        private PortalLimpet _portalData;
        private Dictionary<string, object> _dataObjects;

        public override Dictionary<string, object> ProcessCommand(string paramCmd, SimplisityInfo systemInfo, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
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
                    strOut = GetDetail(_sessionParams.CultureCodeEdit);
                    break;
                case "rocketpluginprojecttemplate_add":
                    strOut = AddpArticle(_sessionParams.CultureCodeEdit);
                    break;
                case "rocketpluginprojecttemplate_delete":
                    strOut = RenderList();
                    break;
                case "rocketpluginprojecttemplate_save":
                    strOut = SavepArticle(_sessionParams.CultureCodeEdit);
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
            var razorTempl = _appThemePortal.GetTemplate("Reload.cshtml");
            var pr = RenderRazorUtils.RazorProcessData(razorTempl, _portalData, _dataObjects, _passSettings, _sessionParams, true);
            if (pr.StatusCode != "00") return pr.ErrorMsg;
            return pr.RenderedText;
        }

        public string InitCmd(string paramCmd, SimplisityInfo systemInfo, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
        {
            _postInfo = postInfo;
            _paramInfo = paramInfo;
            _systemData = new SystemLimpet(systemInfo.GetXmlProperty("genxml/systemkey"));
            _appThemeSystem = new AppThemeSystemLimpet(_systemData.SystemKey);
            _appThemePlugin = new AppThemeSystemLimpet("rocketpluginprojecttemplate");
            _rocketInterface = new RocketInterface(interfaceInfo);
            _sessionParams = new SessionParams(_paramInfo);
            _passSettings = new Dictionary<string, string>();
            _appThemePortal = new AppThemeDNNrocketLimpet("rocketportal");

            // Assign Langauge
            DNNrocketUtils.SetCurrentCulture();
            if (_sessionParams.CultureCode == "") _sessionParams.CultureCode = DNNrocketUtils.GetCurrentCulture();
            if (_sessionParams.CultureCodeEdit == "") _sessionParams.CultureCodeEdit = DNNrocketUtils.GetEditCulture();
            DNNrocketUtils.SetCurrentCulture(_sessionParams.CultureCode);
            DNNrocketUtils.SetEditCulture(_sessionParams.CultureCodeEdit);

            _portalData = new PortalLimpet(PortalUtils.GetPortalId());

            var securityData = new SecurityLimpet(PortalUtils.GetPortalId(), _systemData.SystemKey, _rocketInterface, -1, -1);

            _dataObjects = new Dictionary<string, object>();
            _dataObjects.Add("appthemesystem", _appThemeSystem);
            _dataObjects.Add("appthemeportal", _appThemePortal);
            _dataObjects.Add("appthemeplugin", _appThemePlugin);
            _dataObjects.Add("portaldata", _portalData);
            _dataObjects.Add("securitydata", securityData);
            _dataObjects.Add("systemdata", _systemData);

            if (paramCmd.StartsWith("remote_"))
            {
                var sk = _paramInfo.GetXmlProperty("genxml/remote/securitykeyedit");
                if (!UserUtils.IsEditor() && _portalData.SecurityKeyEdit != sk) paramCmd = "";
            }
            else
            {
                paramCmd = securityData.HasSecurityAccess(paramCmd, "rocketsystem_login");
            }

            return paramCmd;
        }


    }
}
