using UdonSharp;

namespace Varneon.VUdon.Menus.Abstract
{
    /// <summary>
    /// Abstract provider for creating menus
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class MenuProvider : UdonSharpBehaviour
    {
        public abstract bool IsPathRegistered(string path);

        public virtual bool TryRegisterPage(string path, string tooltip = "") { return false; }

        public virtual bool TryRegisterButton(string path, MenuEventCallbackReceiver callbackReceiver, string tooltip = "") { return false; }

        public virtual bool TryRegisterToggle(string path, MenuEventCallbackReceiver callbackReceiver, bool defaultValue, string offOptionName = "Off", string onOptionName = "On", string tooltip = "") { return false; }

        public virtual bool TryRegisterOption(string path, MenuEventCallbackReceiver callbackReceiver, string[] optionNames, int defaultValue, string tooltip = "") { return false; }

        public virtual bool TryRegisterSlider(string path, MenuEventCallbackReceiver callbackReceiver, float defaultValue, float minValue = 0f, float maxValue = 1f, int steps = 10, string unit = "%", string tooltip = "") { return false; }

        public virtual bool TrySetItemEnabled(string path, bool enabled, MenuEventCallbackReceiver callbackReceiver = null) { return false; }

        public virtual bool TryRemoveItem(string path, MenuEventCallbackReceiver callbackReceiver = null) { return false; }

        public virtual bool TrySetToggleValue(string path, bool value) { return false; }

        public virtual bool TrySetOptionValue(string path, int value) { return false; }

        public virtual bool TrySetSliderValue(string path, float value) { return false; }

        public virtual bool TrySetToggleValueWithoutNotify(string path, bool value) { return false; }

        public virtual bool TrySetOptionValueWithoutNotify(string path, int value) { return false; }

        public virtual bool TrySetSliderValueWithoutNotify(string path, float value) { return false; }
    }
}
