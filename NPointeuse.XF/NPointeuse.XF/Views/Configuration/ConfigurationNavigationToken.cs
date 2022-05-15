using NPointeuse.Infra.Client;
using NPointeuse.Infra.XF;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPointeuse.XF.Views.Configuration
{
    internal class ConfigurationNavigationToken : IIconNavigationToken
    {
        public string Title => "Configuration";

        public string Icon => "icon_gear.png";
    }
}
