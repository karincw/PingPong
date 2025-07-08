using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _mapText;

    [SerializeField] private TMP_Text _keyInfo;

    private int _playerScore = 0, _enemyScore = 0;
    private Events _currentEvent = Events.Normal;

    public event Action<Camp, int> OnScoreChangeEvent;

    [Space(4), Header("Effect Settings")]
    [SerializeField] private Transform _eventChangeEffectTrm;
    private TMP_Text _eventChangeEffectText;
    [SerializeField] private Transform _rePlayEffectTrm;
    private TMP_Text _rePlayEffectText;
    [Space(2)]
    [SerializeField] private float _eventChangeEffectTime = 0.4f;
    [SerializeField] private float _rePlayEffectTime = 0.5f;

    private WaitForSeconds oneSecDelay = new WaitForSeconds(1);

    public bool _start = false;

    private void Awake()
    {
        _start = false;
        _eventChangeEffectText = _eventChangeEffectTrm.GetComponentInChildren<TMP_Text>();
        _rePlayEffectText = _rePlayEffectTrm.GetComponentInChildren<TMP_Text>();
    }

    public void SetKeyInfo(string text)
    {
        _keyInfo.text = text;
    }

    public void AddScore(Camp camp, int score)
    {
        int changeScore = 0;
        switch (camp)
        {
            case Camp.Player:
                _playerScore += score;
                changeScore = _enemyScore;
                break;
            case Camp.Enemy:
                _enemyScore += score;
                changeScore = _enemyScore;
                break;
        }
        OnScoreChangeEvent?.Invoke(camp, changeScore);
        _scoreText.text = $"{_playerScore}:{_enemyScore}";
    }
    public void SetEvent(Events evt)
    {
        _currentEvent = evt;
        _mapText.text = $"Map:{_currentEvent.ToString()}";
    }

    public void ReplayEffect()
    {
        _rePlayEffectTrm.gameObject.SetActive(true);
        _rePlayEffectTrm.localPosition = new Vector2(0, 1);
        _rePlayEffectTrm.transform.localScale = Vector3.zero;
        _rePlayEffectText.alpha = 1;
        if (!_start) _rePlayEffectText.text = "Play!";
        _start = true;

        Sequence seq = DOTween.Sequence()
            .Append(_rePlayEffectTrm.DOScale(Vector3.one, _rePlayEffectTime).SetEase(Ease.Linear))
            .Join(_rePlayEffectTrm.DORotate(new Vector3(0, 0, 360), _rePlayEffectTime, RotateMode.FastBeyond360).SetEase(Ease.Linear))
            .AppendInterval(0.2f)
            .Append(_rePlayEffectText.DOFade(0, 0.2f).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                _rePlayEffectTrm.gameObject.SetActive(false);
                _rePlayEffectText.text = "RePlay!";
            });
    }
    public void EventChangeEffect(Events evt)
    {
        _eventChangeEffectTrm.gameObject.SetActive(true);
        _eventChangeEffectTrm.localPosition = new Vector2(0, 6.5f);
        _eventChangeEffectTrm.transform.localScale = Vector3.one * 0.5f;
        _eventChangeEffectText.alpha = 1;
        _eventChangeEffectText.text = $"{evt.ToString()}!!";

        Sequence seq = DOTween.Sequence()
            .Append(_eventChangeEffectTrm.DOScale(Vector3.one, _eventChangeEffectTime).SetEase(Ease.OutBounce))
            .Join(_eventChangeEffectTrm.DOLocalMove(Vector2.up, _eventChangeEffectTime).SetEase(Ease.OutBounce))
            .AppendInterval(0.2f)
            .Append(_eventChangeEffectText.DOFade(0, 0.2f).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                _eventChangeEffectTrm.gameObject.SetActive(false);
            });
    }

}

public enum Camp
{
    Player,
    Enemy
}

public enum Events
{
    Normal,
    Portal,
    Rough,
    Tornado,
    Speeeed,
    Gravity,
}
