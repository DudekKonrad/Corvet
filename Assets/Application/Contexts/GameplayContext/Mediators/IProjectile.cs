using UnityEngine;

namespace Application.Contexts.GameplayContext.Mediators
{
    public interface IProjectile
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
        public void Init(Vector3 direction);
    }
}