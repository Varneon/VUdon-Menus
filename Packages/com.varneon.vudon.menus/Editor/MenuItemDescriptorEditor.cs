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

            descriptor.menu = (MenuProvider)EditorGUILayout.ObjectField("Menu", descriptor.menu, typeof(MenuProvider), true);

            GUILayout.Space(20);

            using (new GUILayout.HorizontalScope())
            {
                if(GUILayout.Button("Collapse All"))
                {
                    Undo.RecordObject(descriptor, "Collapse All Items");

                    foreach(MenuItemDescriptor.Item item in descriptor.menuItems)
                    {
                        item.Expanded = false;
                    }
                }
                else if (GUILayout.Button("Expand All"))
                {
                    Undo.RecordObject(descriptor, "Expand All Items");

                    foreach (MenuItemDescriptor.Item item in descriptor.menuItems)
                    {
                        item.Expanded = true;
                    }
                }
                else if(GUILayout.Button("Sort Items"))
                {
                    Undo.RecordObject(descriptor, "Sort Items");

                    descriptor.menuItems.Sort();
                }
            }

            for (int i = 0; i < descriptor.menuItems.Count; i++)
            {
                MenuItemDescriptor.Item item = descriptor.menuItems[i];

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
