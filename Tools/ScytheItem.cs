// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Core.Utils;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Plants;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.Simulation;
    using Eco.World;

    [Category("Hidden")]
    [Mower]
    public partial class ScytheItem : ToolItem
    {
       
        static ScytheItem() { }
		
		protected new static SkillModifiedValue CreateCalorieValue(float startValue, Type skillType, Type beneficiary, LocString beneficiaryText) {
            return CreateSkillModifiedValue(startValue, calorieMultiplicativeStrategy, skillType, beneficiary, beneficiaryText, Localizer.DoStr("calorie consumption"), typeof(Calorie));
		}
		
        private new static ModificationStrategy calorieMultiplicativeStrategy =
            new InfiniteMultiplicativeStrategy(new float[] { 1, 0.8f, 0.75f, 0.7f, 0.65f, 0.6f, 0.55f, 0.5f });

        protected new static SkillModifiedValue CreateSkillModifiedValue(float startValue, ModificationStrategy strategy, Type skillType, Type beneficiary, LocString beneficiaryText, LocString benefitText, Type modifierType)
        {
            SkillModifiedValue value = new SkillModifiedValue(startValue, strategy, skillType, benefitText, modifierType);
            SkillModifiedValueManager.AddBenefitForObject(beneficiary, beneficiaryText, value);
            SkillModifiedValueManager.AddSkillBenefit(beneficiary, beneficiaryText, value);
            return value;
        }
		
		private static SkillModifiedValue caloriesBurn = CreateCalorieValue(20, typeof(GatheringSkill), typeof(ScytheItem), new ScytheItem().UILink());

        public override IDynamicValue CaloriesBurn            { get { return caloriesBurn; } }

        public override ClientPredictedBlockAction LeftAction { get { return ClientPredictedBlockAction.Harvest; } }
        public override LocString LeftActionDescription          { get { return Localizer.DoStr("Reap"); } }

        static IDynamicValue tier = new ConstantValue(0);
        public override IDynamicValue Tier { get { return tier; } }

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        
        public override Item RepairItem { get { return Item.Get<StoneItem>(); } }
        public override int FullRepairAmount { get { return 1; } }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (!context.HasBlock || !context.Block.Is<Reapable>())
                return InteractResult.NoOp;
            
            var plant = EcoSim.PlantSim.GetPlant(context.BlockPosition.Value);
            if (plant != null && plant is IHarvestable)
            {
                Result result = (plant as IHarvestable).TryHarvest(context.Player, true);
                if (result.Success)
                {
                    this.BurnCalories(context.Player);
                    this.AddExperience(context.Player.User, 1.0f * plant.Species.ExperienceMultiplier, Localizer.Format("harvesting {0}", plant.Species.UILink()));
                    context.Player.SpawnBlockEffect(context.BlockPosition.Value, context.Block.GetType(), BlockEffect.Harvest);
                }

                return (InteractResult)result;
            }
            else
                return (InteractResult)this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false);
        }

        public override bool ShouldHighlight(Type block)
        {
            return Block.Is<Reapable>(block);
        }
    }
}