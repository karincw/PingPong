using UnityEngine;

public class EnemyBar : AgentBar
{
    protected override void Start()
    {
        base.Start();
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

        _movement?.Release();
        _movement = targetMover;
        _movement.Init();
        OnMoverChange?.Invoke(targetMover);
    }

    public void UpHandle()
    {
        _movement.HandleUpButton();
    }
    public void DownHandle()
    {
        _movement.HandleDownButton();
    }
    public void EnterHandle()
    {
        _movement.HandleEnterButton();
    }
}