using System.Collections.Generic;
using SF.Battle.Actors;
using SF.UI.View;
using UnityEngine;

namespace SF.UI.Windows
{
    public class TeamInfoView: MonoBehaviour, IView
    {
        [SerializeField] private CharacterInfoView _characterInfoViewPrefab;

        private readonly List<CharacterInfoView> _infoViews = new List<CharacterInfoView>();
        
        public void CreateHealthPanel(BattleActor actor)
        {
            var healthBarView = Instantiate(_characterInfoViewPrefab, transform);
            
            if (healthBarView == null) return;
            
            healthBarView.ObserveActorHealth(actor);
            _infoViews.Add(healthBarView);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}