using SpotifyToolsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyToolsLib
{
    public abstract class BaseAdapter
    {

        private readonly ISettingsFacade _SettingsFacade;
        public Settings Settings { get; }

        public BaseAdapter(ISettingsFacade settingsFacade)
        {
            _SettingsFacade = settingsFacade;
            Settings = _SettingsFacade.GetSettings();
        }

        public BaseAdapter() : this(new SettingsFacade())
        {
        }


    }
}
