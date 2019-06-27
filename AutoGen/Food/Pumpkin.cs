namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Gameplay.Objects;

    [Serialized]
    [Weight(500)]                                          
    [InfiniteYield(typeof(PumpkinItem), typeof(GatheringSkill), new float[] {1f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f})][Tag("Crop")]      
    [Crop]                                                      
    public partial class PumpkinItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Pumpkin"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Round and large"); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 5, Fat = 0, Protein = 1, Vitamins = 2};
        public override float Calories                          { get { return 340; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}