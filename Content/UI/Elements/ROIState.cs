using Terraria.ModLoader;
using Terraria.UI;

namespace ROI.Content.UI.Elements
{
    public abstract class ROIState : UIState
    {
        protected readonly Mod _mod;

        public ROIState(Mod mod) => _mod = mod;
    }
}