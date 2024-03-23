﻿using UnityEditor;
using UnityEngine;
using Varneon.VUdon.Menus.Abstract;

namespace Varneon.VUdon.Menus.Editor
{
    [CustomEditor(typeof(MenuItemDescriptor))]
    public class MenuItemDescriptorEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            MenuItemDescriptor descriptor = (MenuItemDescriptor)target;

            descriptor.InitializeGUIContent();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("This descriptor is used to add menu items to a MenuProvider", MessageType.Info);

            MenuItemDescriptor descriptor = (MenuItemDescriptor)target;

            Undo.RecordObject(descriptor, "Edit Menu Hierarchy");

            descriptor.menu = (MenuProvider)EditorGUILayout.ObjectField("Menu", descriptor.menu, typeof(MenuProvider), true);

            GUILayout.Space(20);

            for (int i = 0; i < descriptor.MenuItems.Count; i++)
            {
                MenuItemDescriptor.Item item = descriptor.MenuItems[i];

                if (item.DrawInspectorPanel(out bool removed))
                {
                    if (removed)
                    {
                        descriptor.menuItems.RemoveAt(i);

                        break;
                    }

                    descriptor.menuItems[i] = item;
                }
            }

            if (GUILayout.Button("Add Item"))
            {
                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Button"), false, () => descriptor.menuItems.Add(MenuItemDescriptor.Item.Button()));
                menu.AddItem(new GUIContent("Toggle"), false, () => descriptor.menuItems.Add(MenuItemDescriptor.Item.Toggle()));
                menu.AddItem(new GUIContent("Option"), false, () => descriptor.menuItems.Add(MenuItemDescriptor.Item.Option()));
                menu.AddItem(new GUIContent("Slider"), false, () => descriptor.menuItems.Add(MenuItemDescriptor.Item.Slider()));

                menu.ShowAsContext();
            }
        }
    }
}
