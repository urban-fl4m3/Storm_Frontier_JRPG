using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Battle.Turns;
using SF.Common.Data;
using SF.Game;
using SF.Game.Player;
using SF.Game.States;
using SF.Game.Worlds;
using SF.UI.Data;
using SF.UI.Factories;
using SF.UI.Windows;

namespace SF.Battle.States
{
    public class BattleState : GameState, IBattleWorld
    {
        public IBattleActorsHolder ActorsHolder => _actorsHolder;
        public BattleField Field { get; private set; }
        
        private readonly WindowsFactory _windowsFactory;

        private IWindow _battleWindow;
        private TurnManager _turnManager;

        private BattleActorsHolder _actorsHolder;
        private BattleSceneActorFactory _battleSceneActorFactory;

        public BattleState(IServiceLocator services, IPlayerState playerState) : base(services, playerState)
        {
            _windowsFactory = Services.FactoryHolder.Get<WindowsFactory>();
        }

        public override void Enter(IDataProvider dataProvider)
        {
            Field = dataProvider.GetData<BattleField>();
            var enemyData = dataProvider.GetData<IEnumerable<BattleCharacterInfo>>();

            _actorsHolder = new BattleActorsHolder(Services.Logger);
            _battleSceneActorFactory = new BattleSceneActorFactory(Services);
            
            CreateActors(Team.Player, PlayerState.Loadout.GetBattleCharactersData());
            CreateActors(Team.Enemy, enemyData);
            
            _battleWindow = _windowsFactory.Create(Window.Battle, new DataProvider(this, Services));
            _battleWindow.Show();
            
            _turnManager = new TurnManager(Services.Logger, Services.TickProcessor, _battleWindow.Actions, _actorsHolder);
        }

        public override void Exit()
        {
            Services.Logger.Log("Exited battle state");
        }
        
        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> actorsData)
        {
            var placeholders = Field.GetTeamPlaceholders(team);
            var currentPlaceholderIndex = 0;
            
            foreach (var data in actorsData)
            {
                var meta = new BattleMetaData(team, data);
                
                var actor = _battleSceneActorFactory.Create(data.Config.BattleActor, meta, this);
                
                _actorsHolder.AddActor(actor, team);
                
                var placeholder = placeholders[currentPlaceholderIndex];
                actor.SetNewPlaceholder(placeholder);

                currentPlaceholderIndex++;
            }
        }
        
        // private void CreateBattleCamera()
        // {
        //     var smartCamera = new BattleCameraController(_cinemachineView);
        //     ServiceLocator.CameraHolder.Add(smartCamera);
        // }
    }
}