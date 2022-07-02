using SF.Battle.Actors;
using SF.Common.Actors.Components.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class CharacterInfoView : MonoBehaviour
    {
        [SerializeField] private Image _actorIcon;
        [SerializeField] private TextMeshProUGUI _actorName;
        [SerializeField] private TextMeshProUGUI _actorLevel;
        [SerializeField] private CharacterBarView _healthBarView;
        [SerializeField] private CharacterBarView _manaBarView;
        
        private BaseResourceStatComponent _observableHealthComponent;
        private BaseResourceStatComponent _observableManaComponent;
        
        public void ObserveActorHealth(BattleActor actor)
        {
            var visualParameter = actor.MetaData.Info.Config;
            
            _actorName.text = visualParameter.Name;
            _actorLevel.text = $"Lv.{actor.Level}";
            _actorIcon.sprite = visualParameter.Icon;

            _observableHealthComponent = actor.Components.Get<HealthComponent>();
            _observableManaComponent = actor.Components.Get<ManaComponent>();
            
            StartObserveStats();
        }

        private void StartObserveStats()
        {
            TryObserveStat(_healthBarView, _observableHealthComponent);
            TryObserveStat(_manaBarView, _observableManaComponent); 
        }

        private void TryObserveStat(CharacterBarView barView, BaseResourceStatComponent statComponent)
        {
            if (statComponent != null && barView != null)
            {
                barView.StartObserve(statComponent);
            }
        }
        
        private void OnEnable()
        {
            StartObserveStats();
        }

        private void OnDisable()
        {
            if (_healthBarView != null)
            {
                _healthBarView.StopObserve();
            }

            if (_manaBarView != null)
            {
                _manaBarView.StopObserve();
            }
        }
    }
}