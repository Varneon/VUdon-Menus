using System;
using UdonSharp;

namespace Varneon.VUdon.Menus.Abstract
{
    /// <summary>
    /// Abstract provider for creating menus
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class MenuProvider : UdonSharpBehaviour
    {
        protected const float
            DEFAULT_MIN_FLOAT = 0f,
            DEFAULT_MAX_FLOAT = 1f;

        protected const int DEFAULT_STEPS = 10;

        protected const string
            DEFAULT_OFF_LABEL = "Off",
            DEFAULT_ON_LABEL = "On",
            DEFAULT_UNIT = "%",
            DEFAULT_TOOLTIP = "",
            PATH_DELIMITER_STRING = "/";

        protected const char PATH_DELIMITER = '/';

        public abstract bool IsPathRegistered(string path);

        [Obsolete]
        public virtual bool TryRegisterPage(string path, string tooltip = DEFAULT_TOOLTIP) { return false; }
        public virtual bool TryRegisterPage(string path, string tooltip = DEFAULT_TOOLTIP, bool enabled = true) { return false; }

        [Obsolete]
        public virtual bool TryRegisterButton(string path, MenuEventCallbackReceiver callbackReceiver, string tooltip = DEFAULT_TOOLTIP) { return false; }
        public virtual bool TryRegisterButton(string path, MenuEventCallbackReceiver callbackReceiver, string tooltip = DEFAULT_TOOLTIP, bool enabled = true) { return false; }

        [Obsolete]
        public virtual bool TryRegisterToggle(string path, MenuEventCallbackReceiver callbackReceiver, bool defaultValue, string offOptionName = DEFAULT_OFF_LABEL, string onOptionName = DEFAULT_ON_LABEL, string tooltip = DEFAULT_TOOLTIP) { return false; }
        public virtual bool TryRegisterToggle(string path, MenuEventCallbackReceiver callbackReceiver, bool defaultValue, string offOptionName = DEFAULT_OFF_LABEL, string onOptionName = DEFAULT_ON_LABEL, string tooltip = DEFAULT_TOOLTIP, bool enabled = true) { return false; }

        [Obsolete]
        public virtual bool TryRegisterOption(string path, MenuEventCallbackReceiver callbackReceiver, string[] optionNames, int defaultValue, string tooltip = DEFAULT_TOOLTIP) { return false; }
        public virtual bool TryRegisterOption(string path, MenuEventCallbackReceiver callbackReceiver, string[] optionNames, int defaultValue, string tooltip = DEFAULT_TOOLTIP, bool enabled = true) { return false; }

        [Obsolete]
        public virtual bool TryRegisterSlider(string path, MenuEventCallbackReceiver callbackReceiver, float defaultValue, float minValue = DEFAULT_MIN_FLOAT, float maxValue = DEFAULT_MAX_FLOAT, int steps = DEFAULT_STEPS, string unit = DEFAULT_UNIT, string tooltip = DEFAULT_TOOLTIP) { return false; }
        public virtual bool TryRegisterSlider(string path, MenuEventCallbackReceiver callbackReceiver, float defaultValue, float minValue = DEFAULT_MIN_FLOAT, float maxValue = DEFAULT_MAX_FLOAT, int steps = DEFAULT_STEPS, string unit = DEFAULT_UNIT, string tooltip = DEFAULT_TOOLTIP, bool enabled = true) { return false; }

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
