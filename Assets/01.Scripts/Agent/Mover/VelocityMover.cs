using UnityEngine;
public class VelocityMover : AgentMover

{
    [SerializeField] private float _moveSpeed = 250;
    private Vector2 direction;

    public override void Init()
    {
        _rigidbody.gravityScale = 0;
        direction = Vector2.zero;
        _enable = true;
    }

    public override void Release()
    {
        _enable = false;
    }

    public void Update()
    {
        if (_enable)
        {
            _rigidbody.velocity = direction * _moveSpeed;
        }
    }

    public override string GetKeyInfo()
    {
        return "W,ก่ : UP\nS,ก้ : Down\nSpacebar,Enter : Stop";
    }

    public override float GetPowerPercent()
    {
        return direction.y;
    }

    public override void HandleDownButton()
    {
        direction = Vector2.down;
    }

    public override void HandleUpButton()
    {
        direction = Vector2.up;
    }

    public override void HandleEnterButton()
    {
        direction = Vector2.zero;
    }
}
