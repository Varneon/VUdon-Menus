using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    public class MenuOptionItemInfo : MenuItemInfo
    {
        public override MenuItemType Type => MenuItemType.Option;

        public string[] Options;

        public int DefaultValue;

        public MenuOptionItemInfo(string path, MenuEventCallbackReceiver callbackReceiver, string[] options, int defaultValue = 0, string tooltip = "", int priority = 0)
        {
            Path = path;

            CallbackReceiver = callbackReceiver;

            Options = options;

            DefaultValue = defaultValue;

            Tooltip = tooltip;

            Priority = priority;
        }
    }
}
