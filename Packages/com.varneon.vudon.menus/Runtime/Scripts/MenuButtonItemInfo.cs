using Varneon.VUdon.Common.VRCEnums;
using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    public class MenuButtonItemInfo : MenuItemInfo
    {
        public override MenuItemType Type => MenuItemType.Button;

        public MenuButtonItemInfo(string path, MenuEventCallbackReceiver callbackReceiver, string tooltip = "", int priority = 0, bool enabled = true, VRCPlatformTypeFlags platformFlags = (VRCPlatformTypeFlags)(-1))
        {
            Path = path;

            CallbackReceiver = callbackReceiver;

            Tooltip = tooltip;

            Priority = priority;

            Enabled = enabled;

            PlatformFlags = platformFlags;
        }
    }
}
