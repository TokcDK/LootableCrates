using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;

namespace LootableCrates.Settings
{
    public class ItemEntry
    {
        public FormLink<ILeveledItemGetter> Item { get; set; } = new();
        public int Count { get; set; } = 0;
    }
}