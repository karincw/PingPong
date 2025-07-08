using System;
using UnityEngine;

public class PlayerBar : AgentBar
{
    [SerializeField] private InputReaderSO _inputReader;
    private float _upDownValue = 0;

    public override void ChangeMover(Mover mover)
    {
        AgentMover targetMover = null;

        switch (mover)
        {
            case Mover.Velocity:
                targetMover = _velocityMover;
                break;
            case Mover.Jump:
                targetMover = _jumpMover;
                break;
        }

        _inputReader.ReleaseAllEvents();
        _movement?.Release();
        _movement = targetMover;
        _movement.Init();
        _inputReader.OnUpdownInput += HandleUPdownEvent;
        _inputReader.OnEnterInput += HandleEnterEvent;
        OnMoverChange?.Invoke(targetMover);
    }

    protected void Update()
    {
        if (_upDownValue > 0)
        {
            _movement.HandleDownButton();
        }
        else if (_upDownValue < 0)
        {
            _movement.HandleUpButton();
        }
    }

    protected void HandleUPdownEvent(float value)
    {
        _upDownValue = value;
    }

    protected void HandleEnterEvent()
    {
        _movement.HandleEnterButton();
    }
}