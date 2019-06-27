namespace Eco.Gameplay.Items
{
	using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.DynamicValues;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Systems.TextLinks;
	
	public class InfiniteYieldAttribute : ItemAttribute
    {
        public SkillModifiedValue Yield { get; private set; }

        public InfiniteYieldAttribute(Type beneficiary, Type skillType)
            : this(beneficiary, Item.Get(beneficiary).UILink(), skillType)
        { }

        public InfiniteYieldAttribute(Type beneficiary, LocString beneficiaryText, Type skillType)
        {
            this.Yield = new SkillModifiedValue(0, new LinearStrategy(), skillType, Localizer.DoStr("harvest yield"), typeof(Yield));
            SkillModifiedValueManager.AddSkillBenefit(beneficiary, beneficiaryText, this.Yield);
            SkillModifiedValueManager.AddBenefitForObject(beneficiary, beneficiaryText, this.Yield);
        }

        public InfiniteYieldAttribute(Type beneficiary, Type skillType, float[] multipliers)
            : this(beneficiary, Item.Get(beneficiary).UILink(), skillType, multipliers)
        { }

        public InfiniteYieldAttribute(Type beneficiary, LocString beneficiaryText, Type skillType, float[] multipliers)
        {
            this.Yield = new SkillModifiedValue(1, new InfiniteMultiplicativeStrategy(multipliers), skillType, Localizer.DoStr("harvest yield"), typeof(Yield));
            SkillModifiedValueManager.AddSkillBenefit(beneficiary, beneficiaryText, this.Yield);
            SkillModifiedValueManager.AddBenefitForObject(beneficiary, beneficiaryText, this.Yield);
        }
    }
	
}