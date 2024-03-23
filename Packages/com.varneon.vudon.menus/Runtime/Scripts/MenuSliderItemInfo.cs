using Varneon.VUdon.Menus.Abstract;
using Varneon.VUdon.Menus.Enums;

namespace Varneon.VUdon.Menus
{
    public class MenuSliderItemInfo : MenuItemInfo
    {
        public override MenuItemType Type => MenuItemType.Slider;

        public float DefaultValue;

        public float MinValue;

        public float MaxValue;

        public int Steps;

        public string Unit;

        public MenuSliderItemInfo(string path, MenuEventCallbackReceiver callbackReceiver, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f, int steps = 10, string unit = "%", string tooltip = "", int priority = 0, bool enabled = true)
        {
            Path = path;

            CallbackReceiver = callbackReceiver;

            DefaultValue = defaultValue;

            MinValue = minValue;

            MaxValue = maxValue;

            Steps = steps;

            Unit = unit;

            Tooltip = tooltip;

            Priority = priority;

            Enabled = enabled;
        }
    }
}
