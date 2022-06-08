using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Game;
using SF.Game.Stats;
using SF.UI.View;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace SF.UI.Windows
{
    public class TeamHealthPanelView: SerializedMonoBehaviour, IView
    {
        [SerializeField] private HealthBarView _healthBarViewPrefab;

        public void CreateHPPanel(BattleActor actor)
        {
            var panel = Instantiate(_healthBarViewPrefab, transform);
            var hpComponent = actor.Components.Get<ActorHPComponent>();
            var hp = actor.PrimaryStats.GetStat(PrimaryStat.HP);

            //Вьюха никогда не должна настраивать компоненты игровых объектов! Вьюхи только читают!
            // hpComponent.SetHP(hp);
            panel.SetHP(hp);

            hpComponent.CurrentHP.Subscribe(panel.ChangeHP);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}