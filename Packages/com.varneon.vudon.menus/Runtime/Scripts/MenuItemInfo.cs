using System;
using Varneon.VUdon.Common.VRCEnums;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus.Abstract
{
    public abstract class MenuItemInfo : IComparable<MenuItemInfo>
    {
        public string Path;

        public MenuEventCallbackReceiver CallbackReceiver;

        public MenuProvider MirrorMenu;

        public string MirrorPath;

        public string Tooltip;

        public int Priority = 0;

        public bool Enabled = true;

        public VRCPlatformTypeFlags PlatformFlags = (VRCPlatformTypeFlags)(-1);

        public abstract MenuItemType Type { get; }

        public int CompareTo(MenuItemInfo other)
        {
            if (other == null) { return 1; }

            int delta = Priority.CompareTo(other.Priority);

            return delta == 0 ? Path.CompareTo(other.Path) : delta;
        }

        public void RegisterMirror(MenuProvider menu, string path)
        {
            MirrorMenu = menu;

            MirrorPath = path;
        }
    }
}
