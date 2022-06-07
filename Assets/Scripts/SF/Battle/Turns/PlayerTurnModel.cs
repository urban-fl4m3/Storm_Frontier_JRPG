using System.Threading;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel
    {
        private IActor _selectedActor;
        private BattleActor _currentActor;
        public IActor SelectedActor => _selectedActor;
        public BattleActor CurrentActor => _currentActor;

        public CancellationTokenSource CancelationToken = new CancellationTokenSource();

        public void SelectActor(IActor actor)
        {
            _selectedActor = actor;
        }

        public void SetCurrentActor(BattleActor actor)
        {
            _currentActor = actor;
        }

        public void Cancel()
        {
            _selectedActor = null;
            
            CancelationToken?.Cancel();
            CancelationToken?.Dispose();

            CancelationToken = new CancellationTokenSource();
        }
        
    }
}