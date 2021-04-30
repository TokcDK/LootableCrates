using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;
using Noggog;

namespace LootableStuff
{
    class Program
    {
        private enum ContainerType
        {
            Empty,
            Ale,
            Armor,
            Clothing,
            Fish,
            Food,
            Jewerly,
            Mead,
            Misc,
            Ore,
            OreRich,
            Potions,
            Weapon,
            Wine
        }

        private static readonly Random Random = new();

        private static Lazy<Settings> _settings = null!;

        private static readonly HashSet<FormLink<IStaticGetter>> SmallCrates = new()
        {
            Skyrim.Static.CrateSmall01,
            Skyrim.Static.CrateSmall02,
            Skyrim.Static.CrateSmall03,
            Skyrim.Static.CrateSmall04,
            Skyrim.Static.CrateSmall01Weathered,
            Skyrim.Static.CrateSmall02Weathered,
            Skyrim.Static.CrateSmall03Weathered,
            Skyrim.Static.CrateSmall04Weathered,
        };

        private static readonly HashSet<FormLink<IStaticGetter>> SmallSnowCrates = new()
        {
            Skyrim.Static.CrateSmall01_LightSN,
            Skyrim.Static.CrateSmall02_LightSN,
            Skyrim.Static.CrateSmall01Weathered_LightSN,
            Skyrim.Static.CrateSmall02Weathered_LightSN,
            Skyrim.Static.CrateSmall03WeatheredLight_SN,
            Skyrim.Static.CrateSmall04WeatheredLight_SN,
            Skyrim.Static.CrateSmall03WeatheredSnow,
            Skyrim.Static.CrateSmall04WeatheredSnow,
        };
        
        private static readonly HashSet<FormLink<IStaticGetter>> LongCrates = new()
        {
            Skyrim.Static.CrateSmallLong01,
            Skyrim.Static.CrateSmallLong02,
            Skyrim.Static.CrateSmallLong03,
            Skyrim.Static.CrateSmallLong04,
            Skyrim.Static.CrateSmallLong01Weathered,
            Skyrim.Static.CrateSmallLong02Weathered,
            Skyrim.Static.CrateSmallLong03Weathered,
            Skyrim.Static.CrateSmallLong04Weathered
        };

        private static readonly HashSet<FormLink<IStaticGetter>> LongSnowCrates = new()
        {
            Skyrim.Static.CrateSmallLong01WeatheredLight_SN,
            Skyrim.Static.CrateSmallLong02WeatheredLight_SN,
            Skyrim.Static.CrateSmallLong01WeatheredSnow,
            Skyrim.Static.CrateSmallLong03WeatheredSnow,
        };
        
        private static readonly HashSet<FormLink<IStaticGetter>> SmallEEcoCrates = new()
        {
            Skyrim.Static.CrateSmall01EECo,
            Skyrim.Static.CrateSmall03EECo
        };

        private static readonly HashSet<FormLink<IStaticGetter>> LongEEcoCrates = new()
        {
            Skyrim.Static.CrateSmallLong01EECo,
            Skyrim.Static.CrateSmallLong04EECo
        };
        
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance.AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetAutogeneratedSettings("Settings", "settings.json", out _settings)
                .SetTypicalOpen(GameRelease.SkyrimSE, "WiZkiD Lootable FireWood Piles_patch.esp")
                .Run(args);
        }

        private static Container AddContainer(IPatcherState<ISkyrimMod, ISkyrimModGetter> state,
            IFormLinkGetter<IStaticGetter> placedBaseLink, ContainerType containerType)
        {
            var placedBase = placedBaseLink.Resolve(state.LinkCache);
            var container =
                state.PatchMod.Containers.AddNew(placedBase.EditorID + $"{containerType.ToString()}" + "Container");
            container.ObjectBounds = placedBase.ObjectBounds.DeepCopy();
            if (placedBase.Model != null)
            {
                var alternateTextures = placedBase.Model.AlternateTextures?.Select(x => x.DeepCopy());
                container.Model = new Model
                {
                    File = placedBase.Model!.File,
                    AlternateTextures = alternateTextures?.ToExtendedList()
                };
            }

            container.Flags |= Container.Flag.Respawns;
            /*if (placedBase.EditorID?.ToLower().Contains("barrel") ?? false)
            {
                container.Name = new TranslatedString("Barrel", Language.English);
                container.MajorFlags = Container.MajorFlag.NavMeshGenerationBoundingBox;
                container.OpenSound = Skyrim.SoundDescriptor.DRScBarrelOpenSD.AsNullable();
                container.CloseSound = Skyrim.SoundDescriptor.DRScBarrelCloseSD.AsNullable();
            }*/
            //else if (placedBase.EditorID?.ToLower().Contains("crate") ?? false)
            //{
            container.Name = new TranslatedString("Crate", Language.English);
            container.OpenSound = Skyrim.SoundDescriptor.DRScCrateOpenSD.AsNullable();
            container.CloseSound = Skyrim.SoundDescriptor.DRScCrateCloseSD.AsNullable();
            //}

            container.Items = containerType switch
            {
                ContainerType.Empty => new ExtendedList<ContainerEntry>(),
                ContainerType.Food => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(2, 9),
                            Item = Skyrim.LeveledItem.LItemBarrelFoodSameSmall
                        }
                    }
                },
                ContainerType.Fish => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(3, 9),
                            Item = Skyrim.LeveledItem.LItemFoodFishList
                        }
                    },
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(3, 9),
                            Item = Skyrim.LeveledItem.LItemFoodSaltSmall
                        }
                    }
                },
                ContainerType.Clothing => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(2, 9),
                            Item = Skyrim.LeveledItem.LItemMiscVendorClothing75
                        }
                    }
                },
                ContainerType.Misc => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(4, 9),
                            Item = Skyrim.LeveledItem.LItemMiscVendorMiscItems75
                        }
                    }
                },
                ContainerType.Mead => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(2, 6),
                            Item = Skyrim.Ingestible.FoodMead
                        }
                    }
                },
                ContainerType.Ale => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 7),
                            Item = Skyrim.Ingestible.Ale
                        }
                    }
                },
                ContainerType.Wine => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 5),
                            Item = Skyrim.Ingestible.FoodWineBottle02
                        }
                    }
                },
                ContainerType.Weapon => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 4),
                            Item = Skyrim.LeveledItem.LootBanditWeapon50
                        }
                    }
                },
                ContainerType.Armor => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 3),
                            Item = Skyrim.LeveledItem.LootBanditArmor25
                        }
                    }
                    
                },
                ContainerType.Jewerly => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 3),
                            Item = Skyrim.LeveledItem.LItemJewelryCirclet25
                        }
                    },
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 4),
                            Item = Skyrim.LeveledItem.LItemJewelryNecklace25
                        }
                    },
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 5),
                            Item = Skyrim.LeveledItem.LItemJewelryRingSmall
                        }
                    },
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(1, 5),
                            Item = Skyrim.LeveledItem.LItemJewelryRing25
                        }
                    }
                },
                ContainerType.Potions => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(2, 5),
                            Item = Skyrim.LeveledItem.LItemPotionAll
                        }
                    }
                },
                ContainerType.Ore => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(4, 10),
                            Item = Skyrim.LeveledItem.LItemOreIron
                        }
                    }
                },
                ContainerType.OreRich => new ExtendedList<ContainerEntry>
                {
                    new()
                    {
                        Item = new ContainerItem
                        {
                            Count = Random.Next(2, 6),
                            Item = Skyrim.LeveledItem.LItemOreSilver
                        }
                    }
                },
                _ => throw new ArgumentOutOfRangeException(nameof(containerType), containerType,
                    $"wrong container type {containerType} for {placedBase.EditorID}")
            };
            return container;
        }

        private static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            void ReplaceStatic(IModContext<ISkyrimMod, ISkyrimModGetter, IPlacedObject, IPlacedObjectGetter> placed, IList<Container> found)
            {
                var placedCopy = placed.GetOrAddAsOverride(state.PatchMod);
                var chance = Random.Next(20);
                var index = Random.Next(1, found.Count);
                switch (chance)
                {
                    case >= 0 and <= 4:
                        placedCopy.Base.SetTo(found[0]);
                        break;
                    default:
                        placedCopy.Base.SetTo(found[index]);
                        break;
                }
                
            }

            Dictionary<IFormLinkGetter<ISkyrimMajorRecordGetter>,List<Container> > crateContainers = new();
            SmallCrates.Select(x =>
            {
                List<Container> toAdd = new()
                {
                    AddContainer(state, x, ContainerType.Empty),
                    AddContainer(state, x, ContainerType.Ale),
                    AddContainer(state, x, ContainerType.Clothing),
                    AddContainer(state, x, ContainerType.Fish),
                    AddContainer(state, x, ContainerType.Food),
                    AddContainer(state, x, ContainerType.Mead),
                    AddContainer(state, x, ContainerType.Misc),
                    AddContainer(state, x, ContainerType.Wine)
                };
                return (x, toAdd);
            }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));

            LongCrates.Select(x =>
            {
                List<Container> toAdd = new()
                {
                    AddContainer(state, x, ContainerType.Empty),
                    AddContainer(state, x, ContainerType.Armor),
                    AddContainer(state, x, ContainerType.Ore),
                    AddContainer(state, x, ContainerType.Weapon)
                };
                return (x, toAdd);
            }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));
            
            SmallEEcoCrates.Select(x =>
            {
                List<Container> toAdd = new()
                {
                    AddContainer(state, x, ContainerType.Empty),
                    AddContainer(state, x, ContainerType.Clothing),
                    AddContainer(state, x, ContainerType.Mead),
                    AddContainer(state, x, ContainerType.Jewerly),
                    AddContainer(state, x, ContainerType.OreRich),
                };
                return (x, toAdd);
            }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));
            
            LongEEcoCrates.Select(x =>
            {
                List<Container> toAdd = new()
                {
                    AddContainer(state, x, ContainerType.Empty),
                    AddContainer(state, x, ContainerType.Armor),
                    AddContainer(state, x, ContainerType.Jewerly),
                    AddContainer(state, x, ContainerType.OreRich),
                    AddContainer(state, x, ContainerType.Weapon),
                };
                return (x, toAdd);
            }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));

            if (_settings.Value.PatchSnowStatics)
            {
                SmallSnowCrates.Select(x =>
                {
                    List<Container> toAdd = new()
                    {
                        AddContainer(state, x, ContainerType.Empty),
                        AddContainer(state, x, ContainerType.Ale),
                        AddContainer(state, x, ContainerType.Clothing),
                        AddContainer(state, x, ContainerType.Fish),
                        AddContainer(state, x, ContainerType.Food),
                        AddContainer(state, x, ContainerType.Mead),
                        AddContainer(state, x, ContainerType.Misc),
                        AddContainer(state, x, ContainerType.Wine)
                    };
                    return (x, toAdd);
                }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));

                LongSnowCrates.Select(x =>
                {
                    List<Container> toAdd = new()
                    {
                        AddContainer(state, x, ContainerType.Empty),
                        AddContainer(state, x, ContainerType.Armor),
                        AddContainer(state, x, ContainerType.Ore),
                        AddContainer(state, x, ContainerType.Weapon)
                    };
                    return (x, toAdd);
                }).ForEach(tuple => crateContainers.Add(tuple.x, tuple.toAdd));
            }
            
            foreach (var placed in state.LoadOrder.PriorityOrder.PlacedObject()
                .WinningContextOverrides(state.LinkCache))
            {
                if (crateContainers.TryGetValue(placed.Record.Base, out var found))
                {
                    ReplaceStatic(placed, found);
                }
            }
        }
    }
}