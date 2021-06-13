using UnityEngine;

namespace Components
{
    public struct MoveComponent
    {
        public Transform Transform { get; set; }
        public float Speed { get; set; }
        public Vector3 Direction { get; set; }
    }
}