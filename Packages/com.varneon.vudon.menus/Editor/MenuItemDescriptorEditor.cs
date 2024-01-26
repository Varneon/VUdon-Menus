using UnityEditor;
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

            descriptor.Menu = (MenuProvider)EditorGUILayout.ObjectField("Menu", descriptor.Menu, typeof(MenuProvider), true);

            GUILayout.Space(20);

            for (int i = 0; i < descriptor.MenuItems.Count; i++)
            {
                MenuItemDescriptor.Item item = descriptor.MenuItems[i];

                if (item.DrawInspectorPanel(out bool removed))
                {
                    if (removed)
                    {
                        descriptor.MenuItems.RemoveAt(i);

                        break;
                    }

                    descriptor.MenuItems[i] = item;
                }
            }

            if (GUILayout.Button("Add Item"))
            {
                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Button"), false, () => descriptor.MenuItems.Add(MenuItemDescriptor.Item.Button()));
                menu.AddItem(new GUIContent("Toggle"), false, () => descriptor.MenuItems.Add(MenuItemDescriptor.Item.Toggle()));
                menu.AddItem(new GUIContent("Option"), false, () => descriptor.MenuItems.Add(MenuItemDescriptor.Item.Option()));
                menu.AddItem(new GUIContent("Slider"), false, () => descriptor.MenuItems.Add(MenuItemDescriptor.Item.Slider()));

                menu.ShowAsContext();
            }
        }
    }
}
