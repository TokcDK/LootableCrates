using System.Collections.Generic;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Synthesis.Settings;

namespace LootableCrates.Settings
{
    public class Settings
    {
        public bool PatchSnowStatics { get; set; } = false;

        public List<CrateLoot> CrateLoot { get; set; } = new();
        // {
        //     new CrateLoot()
        //     {
        //         ItemEntries = new List<ItemEntry>()
        //         {
        //             new ItemEntry()
        //             {
        //                 Count = 1,
        //                 Item = Skyrim.LeveledItem.LItemFoodChild
        //             }
        //         }
        //     },
        //     new CrateLoot()
        //     {
        //         ItemEntries = new List<ItemEntry>()
        //         {
        //             new ItemEntry()
        //             {
        //                 Count = 1,
        //                 Item = Skyrim.LeveledItem.LItemFoodChild
        //             }
        //         }
        //     }
        // };
        [SynthesisOrder]
        [SynthesisTooltip("ssd")]
        public List<int> Ints { get; set; } = new ()
        #region Defaults
        {
            1, 
            2, 
            3, 
            4, 
            5, 
            6, 
            7, 
            8, 
            9, 
            10
        };
        #endregion
    }
}