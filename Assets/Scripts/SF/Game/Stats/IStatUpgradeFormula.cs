namespace SF.Game.Stats
{
    public interface IStatUpgradeFormula
    {
        int GetStatValue(float baseValue, float tier, int level);
    }
}