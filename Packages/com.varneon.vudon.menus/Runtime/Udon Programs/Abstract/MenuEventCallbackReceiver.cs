using UdonSharp;

namespace Varneon.VUdon.Menus.Abstract
{
    /// <summary>
    /// Abstract receiver for handling menu event callbacks
    /// </summary>
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class MenuEventCallbackReceiver : UdonSharpBehaviour
    {
        /// <summary>
        /// Gets invoked when a menu button gets clicked and this receiver has been linked as the receiver for that button
        /// </summary>
        /// <param name="path">Menu item path</param>
        public virtual void OnMenuButtonClicked(string path) { }

        /// <summary>
        /// Gets invoked when a menu toggle's value changes and this receiver has been linked as the receiver for that toggle
        /// </summary>
        /// <param name="path">Menu item path</param>
        /// <param name="newValue">New value of the menu toggle</param>
        public virtual void OnMenuToggleValueChanged(string path, bool newValue) { }

        /// <summary>
        /// Gets invoked when a menu option's value changes and this receiver has been linked as the receiver for that option
        /// </summary>
        /// <param name="path">Menu item path</param>
        /// <param name="newValue">New value of the menu option</param>
        public virtual void OnMenuOptionValueChanged(string path, int newValue) { }

        /// <summary>
        /// Gets invoked when a menu slider's value changes and this receiver has been linked as the receiver for that slider
        /// </summary>
        /// <param name="path">Menu item path</param>
        /// <param name="newValue">New value of the menu slider</param>
        public virtual void OnMenuSliderValueChanged(string path, float newValue) { }
    }
}
