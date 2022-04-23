namespace SF.Game.Stats
{
    public interface IStatUpgradeFormula
    {
        float GetStatValue(int baseValue, int tier, int level);
    }
}