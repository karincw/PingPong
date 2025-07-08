using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
    [SerializeField] private Vector2 _startPos = new Vector2(-5, -1);
    [SerializeField] private float _startBallSpeed = 10;
    private float _timer = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    [ContextMenu("PlayGame")]
    public void PlayGame()
    {
        _rigidbody.velocity = Vector2.zero;
        _timer = 0;
        bool StartLeft = Random.value >= 0.5f;
        if(StartLeft)
        {
            transform.localPosition = _startPos;
            _rigidbody.velocity = new Vector2(Random.value + 0.5f, Random.value - 0.5f).normalized * _startBallSpeed;
        }
        else
        {
            transform.localPosition = new Vector2(-_startPos.x, _startPos.y);
            _rigidbody.velocity = new Vector2(-(Random.value + 0.5f), Random.value - 0.5f).normalized * _startBallSpeed;
        }
    }

    public void AddForce(Vector2 power)
    {
        _rigidbody.AddForce(power, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("AgentBar"))
        {
            Vector2 direction = (transform.position - collision.transform.position);
            _rigidbody.AddForce(direction.normalized, ForceMode2D.Impulse);
        }
    }
}
