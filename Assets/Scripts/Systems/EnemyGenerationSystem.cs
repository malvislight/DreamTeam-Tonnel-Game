using System.Threading;
using System.Threading.Tasks;
using Components;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Systems
{
    public class EnemyGenerationSystem : IEcsInitSystem
    {
        private EcsFilter<MoveComponent> _playerMoveFilter = null;
        private Enemy _enemy;

        public EnemyGenerationSystem(LevelData _levelData)
        {
            _enemy = _levelData.Enemy;
        }
        public void Init()
        {
            Generate();
        }

        private async void Generate()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            while (!source.Token.IsCancellationRequested)
            {
                await Task.Delay((int) (Random.Range(0.5f, 2f) * 1000), source.Token);

                foreach (var filter in _playerMoveFilter)
                {
                    if (source.Token.IsCancellationRequested) return;
                    var moveComponent = _playerMoveFilter.Get1(filter);
                    var player = moveComponent.Transform;
                    if(player == null) return;
                    var spawnPoint = player.position + player.forward * Random.Range(10, 15) +
                                     player.right * Random.Range(-Tunnel.Diagonal / 2, Tunnel.Diagonal / 2) +
                                     player.up * Random.Range(-Tunnel.Diagonal / 2, Tunnel.Diagonal / 2);

                    GameObject.Instantiate(_enemy, spawnPoint, Quaternion.identity);
                }
            }
        }
    }
}