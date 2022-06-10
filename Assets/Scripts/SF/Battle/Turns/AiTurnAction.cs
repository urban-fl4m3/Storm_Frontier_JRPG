using System;
using System.Collections;
using SF.Battle.Actors;
using SF.Game;
using UniRx;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        private IDisposable _temporaryDelaySub;
        
        public AiTurnAction(IServiceLocator services, BattleWorld world) : base(services, world)
        {
            
        }

        protected override void OnStartTurn(BattleActor actor)
        {
            Services.Logger.Log($"Actor {actor} turn completed");

            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void Dispose()
        {
            _temporaryDelaySub?.Dispose();
        }

        private IEnumerator CalculatePoints()
        {
            yield return new WaitForSeconds(1);
            _temporaryDelaySub.Dispose();
            CompleteTurn();
        }
    }
}