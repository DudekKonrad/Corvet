using UnityEngine;

namespace Application.Contexts.GameplayContext.Mediators
{
    public interface ICollideable
    {
        public void Collide(Transform target);
    }
}