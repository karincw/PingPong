using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private List<Image> progress;
    [SerializeField] private Color _playerColor;
    [SerializeField] private Color _enemyColor;
    private int _maxProgress;
    private int _round;

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameManager _gameManager;

    private void Awake()
    {
        _maxProgress = progress.Count + 1;
        _round = 0;
        if(_uiManager == null) _uiManager = FindObjectOfType<UIManager>();
        if(_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
        _uiManager.OnScoreChangeEvent += HandleRoundChange;
    }

    private void OnDestroy()
    {
        _uiManager.OnScoreChangeEvent -= HandleRoundChange;
    }

    private void HandleRoundChange(Camp camp, int score)
    {
        int currentNumber = ++_round % _maxProgress;
        var currentProgress = progress[currentNumber - 1];

        currentProgress.color = (camp == Camp.Player ? _playerColor : _enemyColor);

        if (currentNumber == 7)
        {
            _round = 0;
            _gameManager._canReplay = false;
            _gameManager.ChangeMapEvent();

            progress.ForEach(p =>
                p.color = Color.black
            );
        }
    }
}
