using SF.Battle.Actors;
using SF.Battle.Stats;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;
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
        
        private IPrimaryStatResource _observableHealthResolver;
        private IPrimaryStatResource _observableManaResolver;
        
        public void ObserveActorHealth(BattleActor actor)
        {
            var visualParameter = actor.MetaData.Info.Config;
            
            _actorName.text = visualParameter.Name;
            _actorLevel.text = $"Lv.{actor.Level}";
            _actorIcon.sprite = visualParameter.Icon;

            var statHolder = actor.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();
            
            _observableHealthResolver = statContainer.GetStatResourceResolver(PrimaryStat.HP);
            _observableManaResolver = statContainer.GetStatResourceResolver(PrimaryStat.MP);
            
            StartObserveStats();
        }

        private void StartObserveStats()
        {
            TryObserveStat(_healthBarView, _observableHealthResolver);
            TryObserveStat(_manaBarView, _observableManaResolver); 
        }

        private void TryObserveStat(CharacterBarView barView, IPrimaryStatResource primaryStatResource)
        {
            if (primaryStatResource != null && barView != null)
            {
                barView.StartObserve(primaryStatResource);
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