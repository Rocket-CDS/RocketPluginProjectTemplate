using DNNrocketAPI.Components;
using Newtonsoft.Json.Linq;
using RazorEngine.Text;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketPluginProjectTemplate.Components
{
    public class RocketERMStrainTokens<T> : DNNrocketAPI.render.DNNrocketTokens<T>
    {

        public IEncodedString RocketERMStrainTestToken()
        {
            return new RawString("TEST TOKEN");
        }

    }
}
