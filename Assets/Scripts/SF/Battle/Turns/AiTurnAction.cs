using System;
using System.Collections;
using SF.Common.Actors;
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

        protected override void OnStartTurn()
        {
            Services.Logger.Log($"Actor {ActingActor} turn completed");

            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(true);

            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void Dispose()
        {
            if (ActingActor != null)
            {
                ActingActor.Components.Get<PlaceholderComponent>().SetSelected(false);
            }

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