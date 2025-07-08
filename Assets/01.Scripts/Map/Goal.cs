using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Camp _targetCamp;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
        if (_uiManager == null) _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.attachedRigidbody.CompareTag("Ball"))
        {
            _uiManager.AddScore(_targetCamp, 1);
            _gameManager.RePlayGame();
        }
    }
}
