using Application.Contexts.GameplayContext.Models;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CorvetLeafView : MonoBehaviour
{
    [Inject] private readonly PlayerInputModel _playerInput;

    [SerializeField] private GameObject _leftLeaf;
    [SerializeField] private GameObject _rightLeaf;
    [SerializeField] private float _duration;

    private Vector3 _leftLeafRotation;
    private Vector3 _RightLeafRotation;

    private void Start()
    {
        _leftLeafRotation = _leftLeaf.transform.rotation.eulerAngles;
        _RightLeafRotation = _rightLeaf.transform.rotation.eulerAngles;
    }

    private void Update()
    {
        var playerMovingLeft = _playerInput.Movement.x < 0;
        var playerMovingRight = _playerInput.Movement.x > 0;
        var playerStanding = _playerInput.Movement == Vector2.zero;
        if (playerMovingRight)
        {
            _leftLeaf.transform.DORotate(new Vector3(0, 0, 20), _duration);
            _rightLeaf.transform.DORotate(new Vector3(0, 0, 90), _duration);
        }

        if (playerMovingLeft)
        {
            _leftLeaf.transform.DORotate(new Vector3(0, 0, -80), _duration);
            _rightLeaf.transform.DORotate(new Vector3(0, 0, 16), _duration);
        }

        if (playerStanding)
        {
            _leftLeaf.transform.DORotate(Vector3.zero, _duration);
            _rightLeaf.transform.DORotate(Vector3.zero, _duration); 
        }
    }
}
