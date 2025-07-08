using System;
using UnityEngine;

public class LocalSecPlayerBar : AgentBar
{
    [SerializeField] private InputReaderSO _inputReader;
    private float _upDownValue = 0;

    protected override void Start()
    {
        base.Start();
        _inputReader.EnableLocalPlay();
    }

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

        _inputReader.ReleaseAllSecEvents();
        _movement?.Release();
        _movement = targetMover;
        _movement.Init();
        //UIManager.Instance.SetKeyInfo("P1: W | P2 : Num8 = Up\nP1: S | P2 : Num5 = Down\nP1 : Spacebar | P2 : Num + | Num Enter = Stop");
        _inputReader.OnSecUpdownInput += HandleUPdownEvent;
        _inputReader.OnSecEnterInput += HandleEnterEvent;
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