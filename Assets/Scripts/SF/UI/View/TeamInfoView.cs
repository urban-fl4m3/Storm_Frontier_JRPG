using SF.UI.View;
using UnityEngine;

namespace SF.UI.Windows
{
    public class TeamInfoView: BaseView
    {
        [SerializeField] private CharacterInfoView _characterInfoViewPrefab;
        
        public CharacterInfoView CreateInfoView()
        {
            return Instantiate(_characterInfoViewPrefab, transform);
        }
    }
}