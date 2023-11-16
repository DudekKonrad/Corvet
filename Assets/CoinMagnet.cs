using Application.Contexts.GameplayContext.Mediators;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        var collectable = col.gameObject.GetComponent<ICollectable>();
        collectable?.Collect();
    }
}
