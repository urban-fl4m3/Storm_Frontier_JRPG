using SF.Game.Common;
using UnityEngine;

namespace SF.Game.Stats
{
    public class DefaultStatUpgradeFormula : IStatUpgradeFormula
    {
        public float GetStatValue(int baseValue, int tier, int level)
        {
            var tierConfigurationValue = 1 + Constants.Characters.STAT_TIER_CORRECTOR * tier;
            var newValue = baseValue * Mathf.Pow(tierConfigurationValue, level - Constants.Characters.MIN_LEVEL);
            
            return newValue;
        }
    }
}