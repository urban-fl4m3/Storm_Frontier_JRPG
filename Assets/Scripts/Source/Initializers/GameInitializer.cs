using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Source.Initializers
{
    public class GameInitializer : IAsyncStartable
    {
        private readonly LifetimeScope _scope;

        public GameInitializer(LifetimeScope scope)
        {
            _scope = scope;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            using (LifetimeScope.EnqueueParent(_scope))
            {
                await SceneManager.LoadSceneAsync("Scenes/Roguelike/BattleScene", LoadSceneMode.Additive);
            }
        }
    }
}