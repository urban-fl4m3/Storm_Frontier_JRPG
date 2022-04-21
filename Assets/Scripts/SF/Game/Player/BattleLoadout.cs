using System.Collections.Generic;
using SF.Battle.Data;

namespace SF.Game.Player
{
    public class BattleLoadout
    {
        private readonly List<BattleCharacterInfo> _battleCharacters = new List<BattleCharacterInfo>();
        
        public IEnumerable<BattleCharacterInfo> GetBattleCharactersData()
        {
            return _battleCharacters;
        }

        public void AddCharacter(BattleCharacterInfo characterInfo)
        {
            _battleCharacters.Add(characterInfo);
        }
    }
}