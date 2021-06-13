using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;

namespace Systems
{
    public class CheckPointGeneratorSystem : IEcsInitSystem
    {
        private Vector3[] _points;
        private Checkpoint _checkpoint;
        
        public CheckPointGeneratorSystem(LevelData levelData)
        {
            _points = levelData.LevelInfo.tunnelPoints;
            _checkpoint = levelData.CheckpointPrefab;
        }
        public void Init()
        {
            foreach (var point in _points)
            {
                GameObject.Instantiate(_checkpoint, point, Quaternion.identity);
            }
        }
    }
}