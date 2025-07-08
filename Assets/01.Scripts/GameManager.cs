using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private AgentBar[] _agentBars;
    [SerializeField] private EndPanel _endPanel;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private MapManager _mapManager;

    private readonly int maxEventCount = 6;
    [SerializeField] private List<Events> _eventList;
    private int _eventIdx = 0;

    public event Action<int> OnEventChangeEvent;

    private WaitForSeconds oneSecDelay = new WaitForSeconds(1);
    public bool _canReplay = true;

    private void Awake()
    {
        if (_uiManager == null) _uiManager = FindObjectOfType<UIManager>();
        if (_mapManager == null) _mapManager = FindObjectOfType<MapManager>();
        _eventList = new List<Events>();
        for (int i = 1; i < maxEventCount; i++)
        {
            _eventList.Add((Events)i);
        }
        _eventList.OrderBy(evt => Random.value);
    }

    private void OnEnable()
    {
        _uiManager.OnScoreChangeEvent += HandleScoreChange;
    }

    private void OnDisable()
    {
        _uiManager.OnScoreChangeEvent -= HandleScoreChange;
    }

    private void Start()
    {
        Invoke("RePlayGame", 2);
    }

    public void SetBarMover(Mover mover)
    {
        foreach (var bar in _agentBars)
        {
            bar.ChangeMover(mover);
        }
    }

    private void HandleScoreChange(Camp camp, int score)
    {
        if (score >= 30)
        {
            _endPanel.Open();
        }
    }

    public void RePlayGame()
    {
        StartCoroutine("RePlayCoroutine");
    }
    private IEnumerator RePlayCoroutine()
    {
        yield return new WaitUntil(() => _canReplay);
        _uiManager.ReplayEffect();
        yield return oneSecDelay;
        _ball.PlayGame();
        foreach (var bar in _agentBars)
        {
            bar.ResetBarPosition();
        }
    }

    public void ChangeMapEvent()
    {
        _canReplay = false;
        Events nextEvt = _eventList[_eventIdx++];
        if (_eventIdx >= maxEventCount) _eventIdx = 0;
        StartCoroutine("ChangeMapCoroutine", nextEvt);
    }
    private IEnumerator ChangeMapCoroutine(Events evt)
    {
        _uiManager.EventChangeEffect(evt);
        yield return oneSecDelay;
        _mapManager.ChangeMap(evt);
        _uiManager.SetEvent(evt);
        yield return oneSecDelay;
        OnEventChangeEvent?.Invoke((int)evt);
        yield return oneSecDelay;
        _canReplay = true;
    }
}