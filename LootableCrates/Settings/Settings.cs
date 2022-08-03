using System.Collections.Generic;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Synthesis.Settings;

namespace LootableCrates.Settings
{
    public class Settings
    {
        [SynthesisSettingName("Patch snow statics")]
        [SynthesisTooltip("Make snow statics as containers")]
        [SynthesisDescription("Make snow statics as containers")]
        public bool PatchSnowStatics { get; set; } = false;

        [SynthesisSettingName("Try add extra crate statics from mods")]
        [SynthesisTooltip("Will try to add extra crate statics from mods editor id of which are contains 'cratesmall' keyword")]
        [SynthesisDescription("Will try to add extra crate statics from mods editor id of which are contains 'cratesmall' keyword")]
        public bool CanTryToAddExtraStaticsFromMods { get; set; } = true;

        [SynthesisSettingName("Loot for containers")]
        [SynthesisTooltip("Loot which will be added into crates")]
        [SynthesisDescription("Loot which will be added into crates")]
        public List<CrateLoot> Loot { get; set; } = new()
        {
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemMiscVendorClothing75,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemBarrelFoodSameSmall,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemFoodFishList,
                        Count = 1
                    },
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemFoodSaltSmall,
                        Count = 1
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemMiscVendorMiscItems75,
                        Count = 6
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LootBanditWeapon50,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LootBanditArmor50,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemJewelryCirclet25,
                        Count = 2
                    },
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemJewelryRing25,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemJewelryNecklace25,
                        Count = 3
                    },
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemJewelryRing25,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemPotionAll,
                        Count = 4
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemOreIron,
                        Count = 7
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemOreSilver,
                        Count = 5
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemBarrelIngredientsCommonSame100,
                        Count = 10
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemBarrelIngredientsUncommonSame100,
                        Count = 5
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemBarrelIngredientsUncommonSame100,
                        Count = 5
                    }
                }
            },
            new CrateLoot
            {
                ItemEntries = new List<ItemEntry>
                {
                    new()
                    {
                        Item = Skyrim.LeveledItem.LItemBookClutter,
                        Count = 3
                    }
                }
            },
            new CrateLoot(),
            new CrateLoot(),
            new CrateLoot()
        };
    }
}