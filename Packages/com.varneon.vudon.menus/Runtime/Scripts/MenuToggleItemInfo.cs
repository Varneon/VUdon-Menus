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

        public MenuToggleItemInfo(string path, MenuEventCallbackReceiver callbackReceiver, bool defaultValue = false, string offLabel = "Off", string onLabel = "On", string tooltip = "", int priority = 0)
        {
            Path = path;

            CallbackReceiver = callbackReceiver;

            DefaultValue = defaultValue;

            OffLabel = offLabel;

            OnLabel = onLabel;

            Tooltip = tooltip;

            Priority = priority;
        }
    }
}
