namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
    using Eco.Mods.TechTree;

    // Librarian

    [Serialized]
    [RequiresSkill(typeof(SurvivalistSkill), 0)]
    public class LibrarianSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Librarian"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("The Librarian can create skill books."); } }

        public override void OnLevelUp(User user)
        {
            
        }

        public static ModificationStrategy MultiplicativeStrategy = new MultiplicativeStrategy(new float[] { 1 });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy = new AdditiveStrategy(new float[] { 0 });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public static int[] SkillPointCost = {};

        public override int RequiredPoint { get { return this.Level < SkillPointCost.Length ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < SkillPointCost.Length ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 0; } }
        public override int Tier { get { return 1; } }
    }

    // Access via Book and Scroll
    [Serialized]
    public partial class LibrarianSkillBook : SkillBook<LibrarianSkill, LibrarianSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Librarian Skill Book"); } }
    }

    [Serialized]
    public partial class LibrarianSkillScroll : SkillScroll<LibrarianSkill, LibrarianSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Librarian Skill Scroll"); } }
    }

    // Logging

    [Serialized]
    public partial class LoggingSkillBook : SkillBook<LoggingSkill, LoggingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Logging Skill Book"); } }
    }

    [Serialized]
    public partial class LoggingSkillScroll : SkillScroll<LoggingSkill, LoggingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Logging Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class LoggingSkillBookRecipe : Recipe
    {
        public LoggingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LoggingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodPulpItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Logging Skill Book"), typeof(LoggingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //Hewing

    [Serialized]
    public partial class HewingSkillBook : SkillBook<HewingSkill, HewingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewing Skill Book"); } }
    }

    [Serialized]
    public partial class HewingSkillScroll : SkillScroll<HewingSkill, HewingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewing Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class HewingSkillBookRecipe : Recipe
    {
        public HewingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HewingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Hewing Skill Book"), typeof(HewingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //Mining

    [Serialized]
    public partial class MiningSkillBook : SkillBook<MiningSkill, MiningSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mining Skill Book"); } }
    }

    [Serialized]
    public partial class MiningSkillScroll : SkillScroll<MiningSkill, MiningSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mining Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class MiningSkillBookRecipe : Recipe
    {
        public MiningSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MiningSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Mining Skill Book"), typeof(MiningSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //Mortaring

    [Serialized]
    public partial class MortaringSkillBook : SkillBook<MortaringSkill, MortaringSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortaring Skill Book"); } }
    }

    [Serialized]
    public partial class MortaringSkillScroll : SkillScroll<MortaringSkill, MortaringSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortaring Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class MortaringSkillBookRecipe : Recipe
    {
        public MortaringSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MortaringSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LimestoneItem>(10),
				new CraftingElement<SandstoneItem>(10),
				new CraftingElement<GraniteItem>(10),
				new CraftingElement<ShaleItem>(10),
				new CraftingElement<GneissItem>(10),
				new CraftingElement<BasaltItem>(10),
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Mortaring Skill Book"), typeof(MortaringSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //Gathering

    [Serialized]
    public partial class GatheringSkillBook : SkillBook<GatheringSkill, GatheringSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gathering Skill Book"); } }
    }

    [Serialized]
    public partial class GatheringSkillScroll : SkillScroll<GatheringSkill, GatheringSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gathering Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class GatheringSkillBookRecipe : Recipe
    {
        public GatheringSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GatheringSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PlantFibersItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Gathering Skill Book"), typeof(GatheringSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //AdvancedCampfireCooking

    [Serialized]
    public partial class AdvancedCampfireCookingSkillBook : SkillBook<AdvancedCampfireCookingSkill, AdvancedCampfireCookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("AdvancedCampfireCooking Skill Book"); } }
    }

    [Serialized]
    public partial class AdvancedCampfireCookingSkillScroll : SkillScroll<AdvancedCampfireCookingSkill, AdvancedCampfireCookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("AdvancedCampfireCooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class AdvancedCampfireCookingSkillBookRecipe : Recipe
    {
        public AdvancedCampfireCookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedCampfireCookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TomatoItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("AdvancedCampfireCooking Skill Book"), typeof(AdvancedCampfireCookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

    //Hunting

    [Serialized]
    public partial class HuntingSkillBook : SkillBook<HuntingSkill, HuntingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hunting Skill Book"); } }
    }

    [Serialized]
    public partial class HuntingSkillScroll : SkillScroll<HuntingSkill, HuntingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hunting Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class HuntingSkillBookRecipe : Recipe
    {
        public HuntingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HuntingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(10)
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Hunting Skill Book"), typeof(HuntingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }

}
