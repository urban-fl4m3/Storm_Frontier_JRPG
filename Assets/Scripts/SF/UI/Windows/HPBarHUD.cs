using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Game.Stats;
using SF.UI.View;
using UniRx;
using UnityEngine;

namespace SF.UI.Windows
{
    public class HPBarHUD: MonoBehaviour, IWindow
    {
        [SerializeField] private HPView _hpPanelPrefab;

        public void CreateHPPanel(BattleActor actor)
        {
            var panel = Instantiate(_hpPanelPrefab, transform);
            var hpComponent = actor.Components.Get<ActorHPComponent>();
            var hp = actor.PrimaryStats.GetStat(PrimaryStat.HP);

            hpComponent.SetHP(hp);
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