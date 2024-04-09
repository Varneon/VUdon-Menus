using Varneon.VUdon.Common.VRCEnums;
using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    public class MenuToggleItemInfo : MenuItemInfo
    {
        public override MenuItemType Type => MenuItemType.Toggle;

        public bool DefaultValue;

        public string OffLabel;

        public string OnLabel;

        public MenuToggleItemInfo(string path, MenuEventCallbackReceiver callbackReceiver, bool defaultValue = false, string offLabel = "Off", string onLabel = "On", string tooltip = "", int priority = 0, bool enabled = true, VRCPlatformTypeFlags platformFlags = (VRCPlatformTypeFlags)(-1))
        {
            Path = path;

            CallbackReceiver = callbackReceiver;

            DefaultValue = defaultValue;

            OffLabel = offLabel;

            OnLabel = onLabel;

            Tooltip = tooltip;

            Priority = priority;

            Enabled = enabled;

            PlatformFlags = platformFlags;
        }
    }
}
