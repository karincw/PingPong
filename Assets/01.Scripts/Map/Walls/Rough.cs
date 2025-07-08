using DG.Tweening;
using UnityEngine;

public class Rough : Map
{
    [Space(4), Header("Rough Settings")]
    [SerializeField] private Transform _topWall;
    [SerializeField] private Transform _bottomWall;

    [SerializeField] private float _startDistance;

    public override void SetUp()
    {
        _topWall.transform.localPosition = new Vector3(_topWall.transform.localPosition.x, _startDistance);
        _bottomWall.transform.localPosition = new Vector3(_bottomWall.transform.localPosition.x, -_startDistance);

        Sequence seq = DOTween.Sequence()
            .Append(_topWall.DOLocalMoveY(0, _changingTime).SetEase(Ease.Linear))
            .Join(_bottomWall.DOLocalMoveY(0, _changingTime).SetEase(Ease.Linear));
    }

    public override void Release()
    {
        Sequence seq = DOTween.Sequence()
            .Append(_topWall.DOLocalMoveY(_startDistance, _changingTime).SetEase(Ease.Linear))
            .Join(_bottomWall.DOLocalMoveY(-_startDistance, _changingTime).SetEase(Ease.Linear));
    }
}