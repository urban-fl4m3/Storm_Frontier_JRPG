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
using SF.UI.Models.Actions;
using SF.UI.Windows;

namespace SF.Battle.States
{
    public class BattleState : GameState, IBattleWorld
    {
        public IBattleActorsHolder ActorsHolder => _actorsHolder;
        public BattleField Field { get; private set; }
        public TurnManager Turns { get; private set; }
        
        private readonly WindowsFactory _windowsFactory;

        private IWindow _battleWindow;

        private BattleActorsHolder _actorsHolder;
        private BattleSceneActorFactory _battleSceneActorFactory;

        private readonly ActionBinder _actionBinder = new();
        
        public BattleState(IServiceLocator services, IPlayerState playerState) : base(services, playerState)
        {
            _windowsFactory = Services.FactoryHolder.Get<WindowsFactory>();
        }

        public override void Enter(IDataProvider dataProvider)
        {
            Field = dataProvider.GetData<BattleField>();

            CreateActorsEnvironment();
            CreateAllActors(dataProvider.GetData<IEnumerable<BattleCharacterInfo>>());
            
            Turns = new TurnManager(Services.Logger, Services.TickProcessor, _actionBinder, _actorsHolder);
            Turns.Enable();
            
            CreateHud();
        }

        public override void Exit()
        {
            Services.Logger.Log("Exited battle state");
        }

        private void CreateActorsEnvironment()
        {
            _actorsHolder = new BattleActorsHolder(Services.Logger);
            _battleSceneActorFactory = new BattleSceneActorFactory(Services);
        }

        private void CreateAllActors(IEnumerable<BattleCharacterInfo> enemyData)
        {
            CreateActors(Team.Player, PlayerState.Loadout.GetBattleCharactersData());
            CreateActors(Team.Enemy, enemyData);
        }
        
        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> actorsData)
        {
            var placeholders = Field.GetTeamPlaceholders(team);
            var currentPlaceholderIndex = 0;
            
            foreach (var data in actorsData)
            {
                var meta = new BattleMetaData(team, data);
                
                var actor = _battleSceneActorFactory.Create(data.Config.BattleActor, meta, this);

                var placeholder = placeholders[currentPlaceholderIndex];
                actor.SetNewPlaceholder(placeholder);

                currentPlaceholderIndex++;
                
                _actorsHolder.AddActor(actor, team);
            }
        }

        private void CreateHud()
        {
            _battleWindow = _windowsFactory.Create(Window.Battle, new DataProvider(this, Services, _actionBinder));
            _battleWindow.Show();
        }
        
        // private void CreateBattleCamera()
        // {
        //     var smartCamera = new BattleCameraController(_cinemachineView);
        //     ServiceLocator.CameraHolder.Add(smartCamera);
        // }
    }
}