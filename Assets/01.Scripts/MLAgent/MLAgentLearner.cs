using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MLAgentLearner : Agent
{
    [SerializeField] private EnemyBar _learnBar;
    [SerializeField] private AgentBar _enemyBar;
    [SerializeField] private Ball _ball;
    [SerializeField] private int _mapEvent;
    [SerializeField] private Camp _myCamp;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIManager _uiManager;

    protected override void Awake()
    {
        if (_gameManager == null) _gameManager = FindObjectOfType<GameManager>();
        if (_uiManager == null) _uiManager = FindObjectOfType<UIManager>();
    }

    public override void Initialize()
    {
        _gameManager.OnEventChangeEvent += HandleMapEventChange;
        _uiManager.OnScoreChangeEvent += HandleScoreChange;
    }

    private void HandleMapEventChange(int idx)
    {
        _mapEvent = idx;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_learnBar.transform.localPosition.y);
        sensor.AddObservation(_enemyBar.transform.localPosition.y);
        sensor.AddObservation((Vector2)_ball.transform.localPosition);
        sensor.AddObservation((Vector2)(_ball.Rigidbody.velocity));

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var DiscreteAction = actions.DiscreteActions;

        if (DiscreteAction[0] == 0)
        {
            _learnBar.UpHandle();
        }
        else if (DiscreteAction[0] == 1)
        { 
            _learnBar.DownHandle();
        }
        else if (DiscreteAction[0] == 2)
        {
            _learnBar.EnterHandle();
        }
    }

    private void HandleScoreChange(Camp camp, int a)
    {
        if (_myCamp == camp)
        {
            AddReward(1f);
        }
        else
        {
            AddReward(-1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            AddReward(0.1f);
        }
    }

}
