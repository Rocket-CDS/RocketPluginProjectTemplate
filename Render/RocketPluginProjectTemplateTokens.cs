using DNNrocketAPI.Components;
using RazorEngine.Text;
using Simplisity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketPluginProjectTemplate.Components
{
    public class RocketPluginProjectTemplateTokens<T> : DNNrocketAPI.render.DNNrocketTokens<T>
    {

        public IEncodedString RocketPluginProjectTemplateTestToken()
        {
            return new RawString("TEST TOKEN");
        }

    }
}
