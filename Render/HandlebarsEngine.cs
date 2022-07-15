using DNNrocketAPI;
using DNNrocketAPI.Components;
using HandlebarsDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RocketPortal.Components;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPluginProjectTemplate.Components
{
    public class HandlebarsEngine : HandleBarsInterface
    {
        public override void RegisterHelpers(ref IHandlebars hbs)
        {
            Register_test(ref hbs);
        }
        private static void Register_test(ref IHandlebars hbs)
        {
            hbs.RegisterHelper("testhbs", (writer, context, arguments) =>
            {
                writer.WriteSafeString("TEST HBS");
            });
        }
    }
}
