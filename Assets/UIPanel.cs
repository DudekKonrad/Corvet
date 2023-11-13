using DG.Tweening;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    public void Hide(){
        transform.DOLocalMoveY(1080f, _duration).SetEase(Ease.InBack);
    }

    public void Show()
    {
        transform.DOLocalMoveY(0f, _duration).SetEase(Ease.OutBack);
    }
}
