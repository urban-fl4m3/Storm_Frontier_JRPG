using SF.Game.Data.Characters;

namespace SF.Battle.Data
{
    public readonly struct BattleCharacterInfo
    {
        public GameCharacterConfig Config { get; }
        public int Level { get; }
        
        public BattleCharacterInfo(GameCharacterConfig config, int level)
        {
            Config = config;
            Level = level;
        }
    }
}