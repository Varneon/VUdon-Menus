using JetBrains.Annotations;
using System.Collections.Immutable;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;
using Varneon.VUdon.Menus.Abstract;

namespace Varneon.VUdon.Menus.Editor
{
    public static class MenuBuildPostProcessor
    {
        [UsedImplicitly]
        [PostProcessScene(-1)]
        private static void PostProcessMenus()
        {
            MenuProvider[] menuProviders = Resources.FindObjectsOfTypeAll<MenuProvider>().Where(q => q.gameObject.scene.IsValid()).ToArray();

            MenuItemRegistrar[] menuItemRegistrars = Resources.FindObjectsOfTypeAll<MenuItemRegistrar>().Where(q => q.gameObject.scene.IsValid()).ToArray();

            foreach (MenuProvider menuProvider in menuProviders)
            {
                PostProcessMenuProvider(menuProvider, menuItemRegistrars.Where(r => r.Menu.Equals(menuProvider)).SelectMany(r => r.MenuItems).ToImmutableSortedSet());
            }
        }

        private static void PostProcessMenuProvider(MenuProvider menuProvider, ImmutableSortedSet<MenuItemInfo> menuItems)
        {
            foreach(MenuItemInfo menuItem in menuItems)
            {
                TryRegisterMenuItem(menuProvider, menuItem);
            }
        }

        private static void TryRegisterMenuItem(MenuProvider menuProvider, MenuItemInfo menuItem)
        {
            switch (menuItem.Type)
            {
                case Enums.MenuItemType.Page:
                    menuProvider.TryRegisterPage(menuItem.Path, menuItem.Tooltip);
                    break;
                case Enums.MenuItemType.Button:
                    menuProvider.TryRegisterButton(menuItem.Path, menuItem.CallbackReceiver, menuItem.Tooltip);
                    break;
                case Enums.MenuItemType.Toggle:
                    MenuToggleItemInfo toggleItem = (MenuToggleItemInfo)menuItem;
                    menuProvider.TryRegisterToggle(menuItem.Path, menuItem.CallbackReceiver, toggleItem.DefaultValue, toggleItem.OffLabel, toggleItem.OnLabel, menuItem.Tooltip);
                    break;
                case Enums.MenuItemType.Option:
                    MenuOptionItemInfo optionItem = (MenuOptionItemInfo)menuItem;
                    menuProvider.TryRegisterOption(menuItem.Path, menuItem.CallbackReceiver, optionItem.Options, optionItem.DefaultValue, menuItem.Tooltip);
                    break;
                case Enums.MenuItemType.Slider:
                    MenuSliderItemInfo sliderItem = (MenuSliderItemInfo)menuItem;
                    menuProvider.TryRegisterSlider(menuItem.Path, menuItem.CallbackReceiver, sliderItem.DefaultValue, sliderItem.MinValue, sliderItem.MaxValue, sliderItem.Steps, sliderItem.Unit, menuItem.Tooltip);
                    break;
                default:
                    Debug.LogWarning($"Attempting to add menu item of type <color=red>{menuItem.Type}</color>, which is not supported!");
                    break;
            }
        }
    }
}
