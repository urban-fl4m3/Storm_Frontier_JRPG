using System;
using SF.Battle.Actors;
using SF.Game;
using SF.Game.States;
using SF.UI.Windows;
using UnityEngine;
using UniRx;

namespace SF.UI.Controller
{
    public class BattleHUDController : BattleWorldUiController
    {
        private readonly BattleHUD _hud;

        private IDisposable _activeActorObserver;
        
        public BattleHUDController(BattleHUD hud, IWorld world, GameState state, IServiceLocator serviceLocator) 
            : base(world, state, serviceLocator)
        {
            _hud = hud;
        }

        public void Init()
        {
            _hud.AttackButton.onClick.AddListener(OnAttackClick);
            _hud.SkillButton.onClick.AddListener(OnSkillClick);
            _hud.UseItemButton.onClick.AddListener(OnItemClick);
            _hud.GuardButton.onClick.AddListener(OnGuardClick);

            _activeActorObserver = State.TurnManager.ActiveActor.Subscribe(OnActiveActorChanged);
        }

        private void OnAttackClick()
        {
            Debug.Log("Attack click");
        }

        private void OnSkillClick()
        {
            Debug.Log("Skill click");
        }

        private void OnItemClick()
        {
            Debug.Log("Use Item click");
        }

        private void OnGuardClick()
        {
            Debug.Log("Guard click!");
            
            
        }

        private void OnActiveActorChanged(BattleActor actor)
        {
            if (actor.Team == Team.Player)
            {
                _hud.Show();
            }
            else
            {
                _hud.Hide();
            }
        }
    }
}