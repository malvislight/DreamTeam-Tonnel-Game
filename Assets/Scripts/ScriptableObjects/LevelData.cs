using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data", order = 1)]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Checkpoint _checkpointPrefab;

        public LevelInfo LevelInfo { get; set; }
        public PlayerData PlayerData => _playerData;
        public Enemy Enemy => _enemy;
        public Checkpoint CheckpointPrefab => _checkpointPrefab;
    }
}