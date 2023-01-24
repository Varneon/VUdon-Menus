using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    public class MenuPageItemInfo : MenuItemInfo
    {
        public override MenuItemType Type => MenuItemType.Page;

        public MenuPageItemInfo(string path, string tooltip = "", int priority = 0)
        {
            Path = path;

            Tooltip = tooltip;

            Priority = priority;
        }
    }
}
