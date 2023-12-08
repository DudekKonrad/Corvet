using Application.Contexts.GameplayContext.Mediators;
using Application.Contexts.GameplayContext.Models;
using Application.Contexts.ProjectContext.Configs;
using UnityEngine.Pool;
using Zenject;

namespace Application.Contexts.GameplayContext.Services
{
    public class ProjectilesService
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly CorvetGameConfig _gameConfig;
        [Inject] private readonly PlayerModel _playerModel;
        
        private ObjectPool<IProjectile> _pool;
        
        public ObjectPool<IProjectile> Pool => _pool;

        [Inject]
        private void Construct()
        {
            _pool = new ObjectPool<IProjectile>(OnCreateProjectile,OnGetProjectile, OnReleaseProjectile);
        }

        public IProjectile GetProjectile()
        {
            return _pool.Get();
        }
        
        private void OnGetProjectile(IProjectile projectile)
        {
            projectile.Transform.position = _playerModel.Position;
        }

        private void OnReleaseProjectile(IProjectile projectile)
        {
            projectile.GameObject.SetActive(false);
        }

        private IProjectile OnCreateProjectile()
        {
            var projectile = _diContainer.InstantiatePrefabForComponent<IProjectile>(_gameConfig.Projectiles[0].Prefab, 
                _playerModel.Transform);
            return projectile;
        }
    }
}