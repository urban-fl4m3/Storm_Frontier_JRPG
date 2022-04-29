using SF.Game.Common;
using UnityEngine;

namespace SF.Game.Stats
{
    public class DefaultStatUpgradeFormula : IStatUpgradeFormula
    {
        public int GetStatValue(float baseValue, float tier, int level)
        {
            var tierConfigurationValue = 1 + Constants.Characters.STAT_TIER_CORRECTOR * tier;
            var newValue = baseValue * Mathf.Pow(tierConfigurationValue, level - Constants.Characters.MIN_LEVEL);
            var growthAmplitude = level * Constants.Characters.STAT_GROWTH_AMPLITUDE;
            
            return Mathf.FloorToInt(newValue + growthAmplitude);
        }
    }
}