using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMover : AgentMover
{
    [SerializeField] private float _minPower = -1000;
    [SerializeField] private float _maxPower = 1000;
    [SerializeField] private float _currentPower;
    [SerializeField] private float _powerUpSpeed = 1000;

    public override void Init()
    {
        _rigidbody.gravityScale = 0.6f;
        _enable = true;
    }

    public override void Release()
    {
        _enable = false;
    }

    public override string GetKeyInfo()
    {
        return "W,ก่ : UP-Charge\nS,ก้ : Down-Charge\nSpacebar,Enter : Launch";
    }

    public override float GetPowerPercent()
    {
        return _currentPower / _maxPower;
    }

    public override void HandleUpButton()
    {
        if (_currentPower < 0) _currentPower = 0;
        _currentPower = Mathf.Clamp(_currentPower + _powerUpSpeed * Time.deltaTime, _minPower, _maxPower);
    }

    public override void HandleDownButton()
    {
        if (_currentPower > 0) _currentPower = 0;
        _currentPower = Mathf.Clamp(_currentPower - _powerUpSpeed * Time.deltaTime, _minPower, _maxPower);
    }

    public override void HandleEnterButton()
    {
        _currentPower = _currentPower > 0 ? _currentPower + 150 : _currentPower;
        _rigidbody.AddForce(Vector2.up * _currentPower, ForceMode2D.Impulse);
        _currentPower = 0;
    }
}
