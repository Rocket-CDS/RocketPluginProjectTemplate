using DNNrocketAPI;
using DNNrocketAPI.Components;
using RocketPortal.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RocketPluginProjectTemplate.Components
{
    public class Events : IEventAction
    {
        public Dictionary<string, object> AfterEvent(string paramCmd, SystemLimpet systemData, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
        {
            return new Dictionary<string, object>();
        }

        public Dictionary<string, object> BeforeEvent(string paramCmd, SystemLimpet systemData, SimplisityInfo interfaceInfo, SimplisityInfo postInfo, SimplisityInfo paramInfo, string langRequired = "")
        {
            return new Dictionary<string, object>();
        }
    }
}
