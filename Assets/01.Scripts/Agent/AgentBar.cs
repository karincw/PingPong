using System;
using UnityEngine;

public abstract class AgentBar : MonoBehaviour
{
    protected VelocityMover _velocityMover;
    protected JumpMover _jumpMover;
    protected AgentMover _movement;
    private Vector2 _startPos;
    public Rigidbody2D Rigidbody => _movement.Rigidbody;

    public Action<AgentMover> OnMoverChange;

    protected virtual void Awake()
    {
        _startPos = transform.localPosition;
        _velocityMover = GetComponent<VelocityMover>();
        _jumpMover = GetComponent<JumpMover>();
    }

    protected virtual void Start()
    {
        ChangeMover(Mover.Velocity);
    }

    public abstract void ChangeMover(Mover targetMover);
    public virtual void ResetBarPosition()
    {
        transform.localPosition = _startPos;
    }
}

public enum Mover
{
    Velocity,
    Jump
}

