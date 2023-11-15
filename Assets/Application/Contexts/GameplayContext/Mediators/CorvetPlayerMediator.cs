using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Application.Contexts.GameplayContext.Mediators
{
    public class CorvetPlayerMediator : MonoBehaviour
    {
        [UsedImplicitly]
        private void OnPoint(InputValue value)
        {
            var inputValue = value.Get<Vector2>();
        }
    }
}
