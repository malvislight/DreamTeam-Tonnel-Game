using Components;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Systems
{
    public class LevelInitSystem : IEcsInitSystem
    {
        EcsWorld _world = null;
        private LevelData _levelData;

        public LevelInitSystem(LevelData levelData)
        {
            _levelData = levelData;
        }

        public void Init()
        { 
            var player = _world.NewEntity();
            ref var moveComponent = ref player.Get<MoveComponent>();
            ref var playerComponent = ref player.Get<PlayerComponent>();

            foreach (var points in _levelData.LevelInfo.tunnelPoints)
            {
                Tunnel.Points.Add(points);
            }

            Tunnel.Diagonal = _levelData.LevelInfo.tunnelDiagonal;

            playerComponent.Player = GameObject.Instantiate(_levelData.PlayerData.Prefab, _levelData.LevelInfo.tunnelPoints[0], Quaternion.identity);
            playerComponent.CheckpointDetector = playerComponent.Player.AddComponent<CheckpointDetector>();
            
            moveComponent.Transform = playerComponent.Player.transform;
            moveComponent.Speed = _levelData.PlayerData.Speed;
        }
    }
}