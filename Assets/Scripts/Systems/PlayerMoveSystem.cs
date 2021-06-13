using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerMoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<MoveComponent, PlayerComponent> _playerMoveFilter = null;
        public void Run()
        {
            foreach (var filter in _playerMoveFilter)
            {

                ref var moveComponent = ref _playerMoveFilter.Get1(filter);

                moveComponent.Transform.Translate(Vector3.forward * (moveComponent.Speed * Time.deltaTime));
            }
        }

        public void Init()
        {
            foreach (var filter in _playerMoveFilter)
            {
                ref var playerComponent = ref _playerMoveFilter.Get2(filter);

                playerComponent.CheckpointDetector.OnDetect += SetDirection;
            }
            SetDirection();
        }

        private void SetDirection()
        {
            foreach (var filter in _playerMoveFilter)
            {
                ref var moveComponent = ref _playerMoveFilter.Get1(filter);

                moveComponent.Direction = Tunnel.Points.MoveNext() - moveComponent.Transform.position;
                moveComponent.Transform.forward = moveComponent.Direction.normalized;
            }
        }
    }
}