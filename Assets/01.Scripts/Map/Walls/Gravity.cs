using DG.Tweening;
using UnityEngine;

public class Gravity : Map
{
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
    }

    public override void SetUp()
    {
        _gameManager.SetBarMover(Mover.Jump);
    }

    public override void Release()
    {
        _gameManager.SetBarMover(Mover.Velocity);
    }
}