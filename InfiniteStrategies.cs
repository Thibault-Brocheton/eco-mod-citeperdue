namespace Eco.Gameplay.DynamicValues
{
    using System;
    using Eco.Shared;
    using Eco.Shared.Utils;

	public class InfiniteMultiplicativeStrategy : ModificationStrategy
    {
        public float[] Factors;

        public InfiniteMultiplicativeStrategy(float[] factors) { this.Factors = factors; }

        public override float ModifiedValue(float value, int level) {
			int currentLevel = level;
			if (level >= this.Factors.Length) {
				currentLevel = this.Factors.Length - 1;
			}
			return Mathf.Round(value * this.Factors[currentLevel], 2);
		}
        public override string StyleBonusValue(float bonusValue) {
			return Text.Percent(bonusValue);
		}
        public override float BonusValue(int level) {
			int currentLevel = level;
			if (level >= this.Factors.Length) {
				currentLevel = this.Factors.Length - 1;
			}
			return  Mathf.Abs(1 - this.Factors[currentLevel]);
		}
        public override bool Increases() {
			return this.Factors[1] > 1;
		}
    }
	
	public class InfiniteAdditiveStrategy : ModificationStrategy
    {
        public float[] Additions;

        public InfiniteAdditiveStrategy(float[] additions) { this.Additions = additions; }

        public override float ModifiedValue(float value, int level) {
			int currentLevel = level;
			if (level >= this.Additions.Length) {
				currentLevel = this.Additions.Length - 1;
			}
			return value + this.Additions[currentLevel];
		}
        public override string StyleBonusValue(float bonusValue) {
			return Text.Num(bonusValue);
		}
        public override float BonusValue(int level) {
			int currentLevel = level;
			if (level >= this.Additions.Length) {
				currentLevel = this.Additions.Length - 1;
			}
			return Mathf.Abs(this.Additions[currentLevel]);
		}
        public override bool Increases() {
			return this.Additions[2] > 0;
		}
    }
	
}
