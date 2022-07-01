using System;
using System.Collections;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Game;
using UniRx;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        private IDisposable _temporaryDelaySub;
        private IActor _currentActor;
        
        public AiTurnAction(IServiceLocator services, BattleWorld world) : base(services, world)
        {
            
        }

        protected override void OnStartTurn(BattleActor actor)
        {
            Services.Logger.Log($"Actor {actor} turn completed");
            
            _currentActor = actor;
            
            _currentActor.Components.Get<PlaceholderComponent>().SetSelected(true);
            
            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void Dispose()
        {
            _currentActor?.Components.Get<PlaceholderComponent>().SetSelected(false);

            _temporaryDelaySub?.Dispose();
            _currentActor = null;
        }

        private IEnumerator CalculatePoints()
        {
            yield return new WaitForSeconds(1);
            _temporaryDelaySub.Dispose();
            CompleteTurn();
        }
    }
}