using Systems;
using Leopotam.Ecs;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelStartUp : MonoBehaviour
{
    [SerializeField] private AssetReference _levelData;
    EcsWorld _world;
    private EcsSystems _systems;
    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        
        Addressables.LoadAssetAsync<LevelData>(_levelData).Completed += levelData =>
        {
            levelData.Result.LevelInfo = JsonSerializer.GetLevelInfo();
            _systems.Add(new LevelInitSystem(levelData.Result));
            _systems.Add(new CheckPointGeneratorSystem(levelData.Result));
            _systems.Add(new PlayerMoveSystem());
            _systems.Add(new TonnelGenerationSystem());
            _systems.Add(new EnemyGenerationSystem(levelData.Result));
            
            _systems.Init();
        };
    }

    private void Update()
    {
        _systems.Run();
    }
}