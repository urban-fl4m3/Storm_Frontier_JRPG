using SF.Game.Data;

namespace SF.Battle.Data
{
    public readonly struct BattleCharacterInfo
    {
        public CharacterConfig Config { get; }
        public int Level { get; }
        
        public BattleCharacterInfo(CharacterConfig config, int level)
        {
            Config = config;
            Level = level;
        }
    }
}