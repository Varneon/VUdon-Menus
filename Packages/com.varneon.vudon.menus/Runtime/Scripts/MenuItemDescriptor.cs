using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    [AddComponentMenu("VUdon/Menu Item Descriptor")]
    public class MenuItemDescriptor : MenuItemRegistrar
    {
        public override MenuProvider Menu => menu;

        public override MenuItemInfo[] MenuItems => menuItems.Select(i => i.GetInfo()).ToArray();

        [SerializeField]
        [FormerlySerializedAs("Menu")]
        internal MenuProvider menu;

        [SerializeField]
        [FormerlySerializedAs("MenuItems")]
        internal List<Item> menuItems = new List<Item>();

        private static GUIContent
            ButtonIcon,
            ToggleIcon,
            SliderIcon,
            OptionIcon;

        internal void InitializeGUIContent()
        {
#if UNITY_EDITOR
            ButtonIcon = UnityEditor.EditorGUIUtility.IconContent("d_Button Icon");
            ToggleIcon = UnityEditor.EditorGUIUtility.IconContent("d_Toggle Icon");
            SliderIcon = UnityEditor.EditorGUIUtility.IconContent("d_Slider Icon");
            OptionIcon = UnityEditor.EditorGUIUtility.IconContent("d_Dropdown Icon");
#endif
        }

        [Serializable]
        internal class Item
        {
            [SerializeField]
            internal string Path;

            [SerializeField]
            internal int Priority;

            [SerializeField]
            internal string Tooltip;

            [SerializeField]
            internal bool Active = true;

            [SerializeField]
            internal MenuEventCallbackReceiver CallbackReceiver;

            [SerializeField]
            internal MenuItemType ItemType;

            [SerializeField]
            internal bool DefaultBoolean;

            [SerializeField]
            internal string OffLabel = "OFF";

            [SerializeField]
            internal string OnLabel = "ON";

            [SerializeField]
            internal string[] Options = new string[1];

            [SerializeField]
            internal int OptionCount = 1;

            [SerializeField]
            internal bool OptionsExpanded = true;

            [SerializeField]
            internal int DefaultOption;

            [SerializeField]
            internal float DefaultFloat;

            [SerializeField]
            internal float MinValue;

            [SerializeField]
            internal float MaxValue = 1f;

            [SerializeField]
            internal int Steps = 10;

            [SerializeField]
            internal string Unit = "%";

            [SerializeField]
            internal bool Expanded = true;

            internal GUIContent HeaderContent;

            private Item(MenuItemType type)
            {
                ItemType = type;
            }

            internal static Item Button() => new Item(MenuItemType.Button);
            internal static Item Toggle() => new Item(MenuItemType.Toggle);
            internal static Item Option() => new Item(MenuItemType.Option);
            internal static Item Slider() => new Item(MenuItemType.Slider);

            private static GUIContent GetItemHeaderContent(MenuItemType type, string name)
            {
                name = " " + (string.IsNullOrWhiteSpace(name) ? "<UNNAMED>" : name);

                switch (type)
                {
                    case MenuItemType.Button: return new GUIContent(name, ButtonIcon.image);
                    case MenuItemType.Toggle: return new GUIContent(name, ToggleIcon.image);
                    case MenuItemType.Option: return new GUIContent(name, OptionIcon.image);
                    case MenuItemType.Slider: return new GUIContent(name, SliderIcon.image);
                    default: return GUIContent.none;
                }
            }

            public MenuItemInfo GetInfo()
            {
                switch (ItemType)
                {
                    case MenuItemType.Button: return new MenuButtonItemInfo(Path, CallbackReceiver, Tooltip, Priority);
                    case MenuItemType.Toggle: return new MenuToggleItemInfo(Path, CallbackReceiver, DefaultBoolean, OffLabel, OnLabel, Tooltip, Priority);
                    case MenuItemType.Option: return new MenuOptionItemInfo(Path, CallbackReceiver, Options, DefaultOption, Tooltip, Priority);
                    case MenuItemType.Slider: return new MenuSliderItemInfo(Path, CallbackReceiver, DefaultFloat, MinValue, MaxValue, Steps, Unit, Tooltip, Priority);
                    default: throw new NotImplementedException();
                }
            }

#if UNITY_EDITOR
            internal bool DrawInspectorPanel(out bool removed)
            {
                if (HeaderContent == null || HeaderContent.text != Path)
                {
                    HeaderContent = GetItemHeaderContent(ItemType, Path);
                }

                bool changed;

                removed = false;

                using (UnityEditor.EditorGUI.ChangeCheckScope changedScope = new UnityEditor.EditorGUI.ChangeCheckScope())
                {
                    if(Expanded = UnityEditor.EditorGUILayout.BeginFoldoutHeaderGroup(Expanded, HeaderContent))
                    {
                        using (new GUILayout.VerticalScope(UnityEditor.EditorStyles.helpBox))
                        {
                            using (new GUILayout.HorizontalScope())
                            {
                                Path = UnityEditor.EditorGUILayout.DelayedTextField("Path", Path);

                                if(GUILayout.Button("X", GUILayout.Width(25))) { removed = true; }
                            }

                            Tooltip = UnityEditor.EditorGUILayout.DelayedTextField("Tooltip", Tooltip);

                            CallbackReceiver = (MenuEventCallbackReceiver)UnityEditor.EditorGUILayout.ObjectField("Callback Receiver", CallbackReceiver, typeof(MenuEventCallbackReceiver), true);

                            Priority = UnityEditor.EditorGUILayout.DelayedIntField("Priority", Priority);

                            // Active state isn't applied on build yet
                            //Active = UnityEditor.EditorGUILayout.Toggle("Active", Active);

                            switch (ItemType)
                            {
                                case MenuItemType.Toggle: DrawToggleInspector(); break;
                                case MenuItemType.Option: DrawOptionInspector(); break;
                                case MenuItemType.Slider: DrawSliderInspector(); break;
                            }
                        }
                    }

                    UnityEditor.EditorGUILayout.EndFoldoutHeaderGroup();

                    changed = changedScope.changed;
                }

                return changed;
            }

            private void DrawToggleInspector()
            {
                DefaultBoolean = UnityEditor.EditorGUILayout.Toggle("Default Value", DefaultBoolean);

                OffLabel = UnityEditor.EditorGUILayout.DelayedTextField("Off Label", OffLabel);

                OnLabel = UnityEditor.EditorGUILayout.DelayedTextField("On Label", OnLabel);
            }

            private void DrawSliderInspector()
            {
                MinValue = UnityEditor.EditorGUILayout.DelayedFloatField("Min Value", MinValue);

                if(MinValue >= MaxValue) { MinValue = MaxValue; }

                MaxValue = UnityEditor.EditorGUILayout.DelayedFloatField("Max Value", MaxValue);

                if(MaxValue <= MinValue) { MaxValue = MinValue; }

                Steps = UnityEditor.EditorGUILayout.DelayedIntField("Steps", Steps);

                if(Steps < 2) { Steps = 2; }

                DefaultFloat = UnityEditor.EditorGUILayout.Slider("Default Value", DefaultFloat, MinValue, MaxValue);
            }

            private void DrawOptionInspector()
            {
                using (new UnityEditor.EditorGUI.IndentLevelScope(1))
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        if (OptionsExpanded = UnityEditor.EditorGUILayout.Foldout(OptionsExpanded, "Options"))
                        {
                            using (UnityEditor.EditorGUI.ChangeCheckScope changedScope = new UnityEditor.EditorGUI.ChangeCheckScope())
                            {
                                OptionCount = UnityEditor.EditorGUILayout.DelayedIntField(OptionCount, GUILayout.Width(64));

                                if (changedScope.changed)
                                {
                                    if (OptionCount < 1) { OptionCount = 1; }

                                    if (DefaultOption > OptionCount - 1) { DefaultOption = OptionCount - 1; }

                                    string[] newOptions = new string[OptionCount];

                                    Array.Copy(Options, newOptions, Math.Min(Options.Length, OptionCount));

                                    Options = newOptions;
                                }
                            }
                        }
                    }

                    if (OptionsExpanded)
                    {
                        for (int i = 0; i < OptionCount; i++)
                        {
                            Options[i] = UnityEditor.EditorGUILayout.DelayedTextField(i.ToString(), Options[i]);
                        }
                    }
                }

                DefaultOption = UnityEditor.EditorGUILayout.Popup("Default Value", DefaultOption, Options);
            }
#endif
        }
    }
}
