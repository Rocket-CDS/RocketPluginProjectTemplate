using DNNrocketAPI.Components;
using Rocket.AppThemes.Components;
using RocketPortal.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RocketPluginProjectTemplate.Components
{
    public class DataObjectLimpet
    {
        private Dictionary<string, object> _dataObjects;
        private Dictionary<string, string> _passSettings;
        private string _systemKey;
        public DataObjectLimpet(int portalid, string moduleRef, SessionParams sessionParams, string systemKey, bool editMode = true)
        {
            var cultureCode = sessionParams.CultureCodeEdit;
            if (!editMode) cultureCode = sessionParams.CultureCode;
            Populate(portalid, moduleRef, cultureCode, sessionParams.ModuleId, sessionParams.TabId, systemKey);
        }
        public DataObjectLimpet(int portalid, string moduleRef, string cultureCode, int moduleId, int tabId, string systemKey)
        {
            Populate(portalid, moduleRef,  cultureCode, moduleId, tabId, systemKey);
        }
        public void Populate(int portalid, string moduleRef, string cultureCode, int moduleId, int tabId, string systemKey, bool refresh = false)
        {
            _systemKey = systemKey;
            _passSettings = new Dictionary<string, string>();
            _dataObjects = new Dictionary<string, object>();
            var systemData = SystemSingleton.Instance(_systemKey);

            SetDataObject("systemdata", systemData);
            SetDataObject("appthemesystem", AppThemeUtils.AppThemeSystem(portalid, SystemKey));
            SetDataObject("appthemeplugin", AppThemeUtils.AppThemeSystem(portalid, "rocketpluginprojecttemplate"));
            SetDataObject("portaldata", new PortalLimpet(portalid));
            SetDataObject("appthemeportal", new AppThemeDNNrocketLimpet("rocketportal"));            

        }
        public void SetDataObject(String key, object value)
        {
            if (_dataObjects.ContainsKey(key)) _dataObjects.Remove(key);
            _dataObjects.Add(key, value);
        }
        public object GetDataObject(String key)
        {
            if (_dataObjects != null && _dataObjects.ContainsKey(key)) return _dataObjects[key];
            return null;
        }
        public void SetSetting(string key, string value)
        {
            if (_passSettings.ContainsKey(key)) _passSettings.Remove(key);
            _passSettings.Add(key, value);
        }
        public string GetSetting(string key)
        {
            if (!_passSettings.ContainsKey(key)) return "";
            return _passSettings[key];
        }
        public string SystemKey { get { return _systemKey; } }
        public int PortalId { get { return PortalData.PortalId; } }
        public Dictionary<string, object> DataObjects { get { return _dataObjects; } }
        public AppThemeSystemLimpet AppThemeSystem { get { return (AppThemeSystemLimpet)GetDataObject("appthemesystem"); } }
        public AppThemeSystemLimpet AppThemePlugin { get { return (AppThemeSystemLimpet)GetDataObject("appthemeplugin"); } }
        public AppThemeDNNrocketLimpet AppThemePortal { get { return (AppThemeDNNrocketLimpet)GetDataObject("appthemeportal"); } }
        public PortalLimpet PortalData { get { return (PortalLimpet)GetDataObject("portaldata"); } }
        public SystemLimpet SystemData { get { return (SystemLimpet)GetDataObject("systemdata"); } }
        public Dictionary<string, string> Settings { get { return _passSettings; } }
    }
}
