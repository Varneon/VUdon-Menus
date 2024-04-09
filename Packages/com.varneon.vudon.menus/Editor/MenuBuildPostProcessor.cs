using JetBrains.Annotations;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UdonSharpEditor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Varneon.VUdon.Common.VRCEnums;
using Varneon.VUdon.Menus.Abstract;
using VRC.Udon;

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
                IEnumerable<MenuItemInfo> items = menuItemRegistrars.Where(r => r.Menu.Equals(menuProvider)).SelectMany(r => r.MenuItems);

                PostProcessMenuProvider(menuProvider, items.ToImmutableSortedSet());
            }
        }

        private static void PostProcessMenuProvider(MenuProvider menuProvider, ImmutableSortedSet<MenuItemInfo> menuItems)
        {
            IEnumerable<MenuItemInfo> platformConditionalItems = menuItems.Where(item => item.PlatformFlags != (VRCPlatformTypeFlags)(-1));

            if (platformConditionalItems.Count() > 0)
            {
                GameObject newInitializerObject = new GameObject("MenuInitializer_" + menuProvider.name);

                MenuInitializer initializer = newInitializerObject.AddUdonSharpComponent<MenuInitializer>();

                UdonBehaviour backingUdonBehaviour = UdonSharpEditorUtility.GetBackingUdonBehaviour(initializer);

                if (!BuildPipeline.isBuildingPlayer)
                {
                    UdonManager.Instance.RegisterUdonBehaviour(UdonSharpEditorUtility.GetBackingUdonBehaviour(initializer));
                }

                UdonSharpEditorUtility.GetBackingUdonBehaviour(initializer).SyncMethod = VRC.SDKBase.Networking.SyncType.None;

                initializer.menu = menuProvider;

                initializer.platformFlags = platformConditionalItems.Select(item => (int)item.PlatformFlags).ToArray();

                initializer.menuPaths = platformConditionalItems.Select(item => item.Path).ToArray();
            }

            foreach (MenuItemInfo menuItem in menuItems)
            {
                TryRegisterMenuItem(menuProvider, menuItem);
            }
        }

        private static void TryRegisterMenuItem(MenuProvider menuProvider, MenuItemInfo menuItem)
        {
            switch (menuItem.Type)
            {
                case Enums.MenuItemType.Page:
                    menuProvider.TryRegisterPage(menuItem.Path, menuItem.Tooltip, menuItem.Enabled);
                    break;
                case Enums.MenuItemType.Button:
                    menuProvider.TryRegisterButton(menuItem.Path, menuItem.CallbackReceiver, menuItem.Tooltip, menuItem.Enabled);
                    break;
                case Enums.MenuItemType.Toggle:
                    MenuToggleItemInfo toggleItem = (MenuToggleItemInfo)menuItem;
                    menuProvider.TryRegisterToggle(menuItem.Path, menuItem.CallbackReceiver, toggleItem.DefaultValue, toggleItem.OffLabel, toggleItem.OnLabel, menuItem.Tooltip, menuItem.Enabled);
                    break;
                case Enums.MenuItemType.Option:
                    MenuOptionItemInfo optionItem = (MenuOptionItemInfo)menuItem;
                    menuProvider.TryRegisterOption(menuItem.Path, menuItem.CallbackReceiver, optionItem.Options, optionItem.DefaultValue, menuItem.Tooltip, menuItem.Enabled);
                    break;
                case Enums.MenuItemType.Slider:
                    MenuSliderItemInfo sliderItem = (MenuSliderItemInfo)menuItem;
                    menuProvider.TryRegisterSlider(menuItem.Path, menuItem.CallbackReceiver, sliderItem.DefaultValue, sliderItem.MinValue, sliderItem.MaxValue, sliderItem.Steps, sliderItem.Unit, menuItem.Tooltip, menuItem.Enabled);
                    break;
                default:
                    Debug.LogWarning($"Attempting to add menu item of type <color=red>{menuItem.Type}</color>, which is not supported!");
                    break;
            }
        }
    }
}
