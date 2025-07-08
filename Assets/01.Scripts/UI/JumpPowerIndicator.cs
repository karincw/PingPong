using UnityEngine;

public class JumpPowerIndicator : MonoBehaviour
{
    [SerializeField] private Transform _gaugeBarTrm;
    private SpriteRenderer _gaugeRenderer;
    [SerializeField] private AgentBar _agentBar;

    [SerializeField] private Color _upColor;
    [SerializeField] private Color _downColor;
    private AgentMover _agentMover;

    private float maxSize = 0.5f;

    private void Awake()
    {
        _gaugeRenderer = _gaugeBarTrm.GetComponent<SpriteRenderer>();
        _agentBar.OnMoverChange += SetMover;
    }

    private void OnDestroy()
    {
        _agentBar.OnMoverChange -= SetMover;
    }

    public void SetMover(AgentMover mover)
    {
        _agentMover = mover;
    }

    public void Update()
    {
        var percent = _agentMover.GetPowerPercent();
        var size = maxSize * percent;
        _gaugeRenderer.color = size > 0 ? _upColor : _downColor;
        _gaugeBarTrm.localScale = new Vector2(1, size);
        _gaugeBarTrm.localPosition = new Vector2(0, size * 0.5f);
    }

}
