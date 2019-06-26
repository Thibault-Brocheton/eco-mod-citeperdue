namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
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
	
    [Serialized]
    [RequiresSkill(typeof(ChefSkill), 0)]    
    public partial class CookingSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Cooking"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("The basics of cooking in a more civilized environment give bonuses to a variety of food recipes. Level by crafting related recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1,
                
                1 - 0.5f,
                
                1 - 0.55f,
                
                1 - 0.6f,
                
                1 - 0.65f,
                
                1 - 0.7f,
                
                1 - 0.75f,
                
                1 - 0.8f,
                
            });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0,
                
                0.5f,
                
                0.55f,
                
                0.6f,
                
                0.65f,
                
                0.7f,
                
                0.75f,
                
                0.8f,
                
            });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public static int[] SkillPointCost = {
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
        };
        public override int RequiredPoint { get { return this.Level < SkillPointCost.Length ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < SkillPointCost.Length ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 3; } }
    }

    [Serialized]
    public partial class CookingSkillBook : SkillBook<CookingSkill, CookingSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Book"); } }
    }

    [Serialized]
    public partial class CookingSkillScroll : SkillScroll<CookingSkill, CookingSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cooking Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)]
    public partial class CookingSkillBookRecipe : Recipe
    {
        public CookingSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CookingSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(25),
                new CraftingElement<HewnLogItem>(20),
                new CraftingElement<CampfireRoastItem>(10),
                new CraftingElement<WheatPorridgeItem>(10) 
            };
            this.CraftMinutes = new ConstantValue(15);

            this.Initialize(Localizer.DoStr("Cooking Skill Book"), typeof(CookingSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
