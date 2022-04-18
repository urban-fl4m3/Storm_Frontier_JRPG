namespace SF.Battle.Data
{
    public readonly struct BattleCharacterInfo
    {
        public BattleCharacterConfig Config { get; }
        public int Level { get; }
        
        public BattleCharacterInfo(BattleCharacterConfig config, int level)
        {
            Config = config;
            Level = level;
        }
    }
}