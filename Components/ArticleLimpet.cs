using DNNrocketAPI;
using Simplisity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using DNNrocketAPI.Components;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using System.Xml.Linq;

namespace RocketPluginProjectTemplate.Components
{
    public class ArticleLimpet
    {
        private const string _tableName = "DNNrocket";
        private const string _entityTypeCode = "RocketPluginProjectTemplateART";
        private DNNrocketController _objCtrl;
        private int _articleId;
        private SimplisityInfo _info;

        /// <summary>
        /// Should be used to create an article, the portalId is required on creation
        /// </summary>
        /// <param name="portalId"></param>
        /// <param name="dataRef"></param>
        /// <param name="langRequired"></param>
        public ArticleLimpet(int portalId, string dataRef, string langRequired)
        {
            PortalId = portalId;
            _info = new SimplisityInfo();
            _info.ItemID = -1;
            _info.TypeCode = _entityTypeCode;
            _info.ModuleId = -1;
            _info.UserId = -1;
            _info.GUIDKey = dataRef;
            _info.PortalId = PortalId;

            Populate(langRequired);
        }

        private void Populate(string cultureCode)
        {
            _objCtrl = new DNNrocketController();
            CultureCode = cultureCode;

            var info = _objCtrl.GetByGuidKey(PortalId, -1, _entityTypeCode, DataRef, "", _tableName, cultureCode);
            if (info != null && info.ItemID > 0) _info = info; // check if we have a real record, or a dummy being created and not saved yet.
            _info.Lang = CultureCode;
            PortalId = _info.PortalId;
            if (DataRef == "") DataRef = GeneralUtils.GetGuidKey();

            // Add namespace and json convert to lists. (for handlebars)
            GeneralUtils.AddJsonNetRootAttribute(ref _info);

        }
        public void Delete()
        {
            _objCtrl.Delete(_info.ItemID, _tableName);
        }

        private SimplisityInfo ReplaceInfoFields(SimplisityInfo newInfo, SimplisityInfo postInfo, string xpathListSelect)
        {
            var textList = postInfo.XMLDoc.SelectNodes(xpathListSelect);
            if (textList != null)
            {
                foreach (XmlNode nod in textList)
                {
                    newInfo.SetXmlProperty(xpathListSelect.Replace("*", "") + nod.Name, nod.InnerText);
                }
            }
            return newInfo;
        }
        public int Update()
        {
            // Add namespace and json convert to lists. (for handlebars)
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/rows/genxml/imagelist/genxml", ref _info);
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/lang/genxml/rows/genxml/imagelist/genxml", ref _info);
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/rows/genxml/documentlist/genxml", ref _info);
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/lang/genxml/rows/genxml/documentlist/genxml", ref _info);
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/rows/genxml/linklist/genxml", ref _info);
            GeneralUtils.AddJsonArrayAttributesForXPath("genxml/lang/genxml/rows/genxml/linklist/genxml", ref _info);

            _info = _objCtrl.SaveData(_info, _tableName);
            if (_info.GUIDKey == "")
            {
                _info.GUIDKey = GeneralUtils.GetGuidKey();
                _info = _objCtrl.SaveData(_info, _tableName);
            }
            return _info.ItemID;
        }
        public void RebuildLangIndex()
        {
            _objCtrl.RebuildLangIndex(PortalId, ArticleId, _tableName);
        }
        public int ValidateAndUpdate()
        {
            Validate();
            return Update();
        }
        public int Copy()
        {
            _info.ItemID = -1;
            _info.GUIDKey = GeneralUtils.GetGuidKey();
            return Update();
        }

        public void AddListItem(string listname)
        {
            if (_info.ItemID < 0) Update(); // blank record, not on DB.  Create now.
            _info.AddListItem(listname);         
            Update();
        }
        public void Validate()
        {
        }

        #region "properties"

        public string CultureCode { get; private set; }
        public string EntityTypeCode { get { return _entityTypeCode; } }
        public SimplisityInfo Info { get { return _info; } }
        public int ModuleId { get { return _info.ModuleId; } set { _info.ModuleId = value; } }
        public int XrefItemId { get { return _info.XrefItemId; } set { _info.XrefItemId = value; } }
        public int ParentItemId { get { return _info.ParentItemId; } set { _info.ParentItemId = value; } }
        public int ArticleId { get { return _info.ItemID; } set { _info.ItemID = value; } }
        public string DataRef { get { return _info.GUIDKey; } set { _info.GUIDKey = value; } }
        public string GUIDKey { get { return _info.GUIDKey; } set { _info.GUIDKey = value; } }
        public int SortOrder { get { return _info.SortOrder; } set { _info.SortOrder = value; } }
        public bool DebugMode { get; set; }
        public int PortalId { get; set; }
        public bool Exists { get {if (_info.ItemID  <= 0) { return false; } else { return true; }; } }
        public string LinkListName { get { return "linklist"; } }
        public string DocumentListName { get { return "documentlist"; } }
        public string ImageListName { get { return "imagelist"; } }
        public string Name { get { return _info.GetXmlProperty("genxml/data/name"); } set { _info.SetXmlProperty("genxml/data/name", value); } }
        public string AdminAppThemeFolder { get { return _info.GetXmlProperty("genxml/textbox/apptheme"); } set { _info.SetXmlProperty("genxml/textbox/apptheme", value); } }
        public string AdminAppThemeFolderVersion { get { return _info.GetXmlProperty("genxml/textbox/appthemeversion"); } set { _info.SetXmlProperty("genxml/textbox/appthemeversion", value); } }
        public string Organisation { get { return _info.GetXmlProperty("genxml/select/org"); } set { _info.SetXmlProperty("genxml/select/org", value); } }

        #endregion

    }

}
