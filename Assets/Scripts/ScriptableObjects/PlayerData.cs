using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 1)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int _speed;
        [SerializeField] private GameObject _prefab;

        public GameObject Prefab => _prefab;
        public int Speed => _speed;
    }
}