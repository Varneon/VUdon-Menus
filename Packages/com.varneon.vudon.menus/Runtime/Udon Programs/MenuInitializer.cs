using UdonSharp;
using UnityEngine;
using Varneon.VUdon.Common;
using Varneon.VUdon.Common.VRCEnums;
using Varneon.VUdon.Menus.Abstract;
using VRC.SDKBase;

namespace Varneon.VUdon.Menus
{
    [AddComponentMenu("")]
    [ExcludeFromPreset]
    [DefaultExecutionOrder(int.MaxValue)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class MenuInitializer : UdonSharpBehaviour
    {
        [SerializeField, HideInInspector]
        internal MenuProvider menu;

        [SerializeField, HideInInspector]
        internal int[] platformFlags;

        [SerializeField, HideInInspector]
        internal string[] menuPaths;

        private void Start()
        {
            VRCPlatformType platformType = PlatformCheckUtility.GetCurrentPlatform(out VRCPlayerApi localPlayer, out bool isUserInVR);

            for (int i = 0; i < menuPaths.Length; i++)
            {
                if (PlatformCheckUtility.ComparePlatformFlags((VRCPlatformTypeFlags)platformFlags[i], platformType)) { continue; }

                menu.TryRemoveItem(menuPaths[i]);
            }
        }
    }
}
