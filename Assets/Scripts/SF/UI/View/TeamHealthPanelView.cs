using System.Collections.Generic;
using SF.Common.Actors;
using SF.UI.View;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.UI.Windows
{
    public class TeamHealthPanelView: SerializedMonoBehaviour, IView
    {
        [SerializeField] private HealthBarView _healthBarViewPrefab;

        private readonly List<HealthBarView> _healthBars = new List<HealthBarView>();
        
        public void CreateHPPanel(IActor actor)
        {
            var healthBarView = Instantiate(_healthBarViewPrefab, transform);
            
            if (healthBarView == null) return;
            
            healthBarView.ObserveActorHealth(actor);
            _healthBars.Add(healthBarView);
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