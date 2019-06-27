// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using Shared.Serialization;
    using Eco.Shared.Networking;
    using Simulation;
    using Gameplay.Animals;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Stats;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Objects;
	
    //this is going to be a real item at some point

    [Serialized]
    public partial class FishingPoleItem : ToolItem
    {
        private static IDynamicValue caloriesBurn = new ConstantValue(1.0f);

        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(10, HuntingSkill.MultiplicativeStrategy, typeof(HuntingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        public override LocString DisplayName { get { return Localizer.DoStr("Fishing Pole"); } }

        static FishingPoleItem() { }
		
		protected new static SkillModifiedValue CreateCalorieValue(float startValue, Type skillType, Type beneficiary, LocString beneficiaryText) {
            return CreateSkillModifiedValue(startValue, calorieMultiplicativeStrategy, skillType, beneficiary, beneficiaryText, Localizer.DoStr("calorie consumption"), typeof(Calorie));
		}

        private new static ModificationStrategy calorieMultiplicativeStrategy =
            new InfiniteMultiplicativeStrategy(new float[] { 1, 0.8f, 0.75f, 0.7f, 0.65f, 0.6f, 0.55f, 0.5f});

        protected new static SkillModifiedValue CreateSkillModifiedValue(float startValue, ModificationStrategy strategy, Type skillType, Type beneficiary, LocString beneficiaryText, LocString benefitText, Type modifierType)
        {
            SkillModifiedValue value = new SkillModifiedValue(startValue, strategy, skillType, benefitText, modifierType);
            SkillModifiedValueManager.AddBenefitForObject(beneficiary, beneficiaryText, value);
            SkillModifiedValueManager.AddSkillBenefit(beneficiary, beneficiaryText, value);
            return value;
        }

        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
       
        [RPC]
        void FinalizeCatch(Player player, INetObject target)
        {
            if (target is AnimalEntity)
                if (player.User.Inventory.TryAddItem(((AnimalEntity)target).Species.ResourceItemType, player.User))
                {
                    ((AnimalEntity)target).Kill(DeathType.Harvesting);
                    ((AnimalEntity)target).Destroy();
                }
        }
    }

    public class LureEntity : NetPhysicsEntity
    {
        public LureEntity() : base("Lure") { }

        public override bool IsNotRelevant(INetObjectViewer viewer)
        {
            bool isNot = base.IsNotRelevant(viewer);
            if (this.Controller == null)
                this.Destroy();

            return isNot;
        }

        public override void ReceiveUpdate(BSONObject bsonObj)
        {
            base.ReceiveUpdate(bsonObj);

            if (this.Position.y <= 0)
                this.Destroy();
        }
    }

}