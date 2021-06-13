using UnityEngine;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private Transform _target;
        private Transform _first;
        private Transform _second;
        
        private void Start()
        {
            _first = Instantiate(_player, Vector3.right * 5, Quaternion.identity).transform;
            _target = Instantiate(_player, Vector3.forward * 5, Quaternion.identity).transform;
            _second = Instantiate(_player, Vector3.left * 5, Quaternion.identity).transform;
            
        }

        private void Update()
        {
            
            var bisector = (_target.position - _first.position).normalized + (_target.position - _second.position).normalized;
            
            Debug.DrawLine(_target.position, bisector * 5 + _target.position, Color.red);
            Debug.DrawLine(_target.position, bisector * -5 + _target.position, Color.blue);
            
            Debug.DrawLine(_target.position, _first.position, Color.green);
            Debug.DrawLine(_target.position, _second.position, Color.green);
        }
    }
}