using DG.Tweening;
using UnityEngine;

public class Speed : Map
{
    [Space(4), Header("Speed Settings")]
    [SerializeField] private float _timeScale;
    private float _originTimeScale;

    public override void SetUp()
    {
        _originTimeScale = Time.timeScale;
        Time.timeScale = _timeScale;
    }

    public override void Release()
    {
        Time.timeScale = _originTimeScale;
    }
}