using UnityEngine;

namespace Varneon.VUdon.Menus.Abstract
{
    public abstract class MenuItemRegistrar : MonoBehaviour
    {
        /// <summary>
        /// Menu where the items will be registered
        /// </summary>
        public abstract MenuProvider Menu { get; }

        /// <summary>
        /// Menu items to be registered on build
        /// </summary>
        public abstract MenuItemInfo[] MenuItems { get; }
    }
}
