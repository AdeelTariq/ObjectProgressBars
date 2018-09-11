using Microsoft.Xna.Framework.Input;
using StardewValley;

namespace ObjectProgressBars
{
    public class Configuration
    {
        public InputButton ToggleDisplay
        {
            get;
            set;
        } = new InputButton((Keys)80);

    }
}
