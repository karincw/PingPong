using DG.Tweening;
using System.Collections;
using UnityEngine;

public class TornadoObject : MonoBehaviour
{
    [SerializeField] private float _gravityPower = 1;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private float durationTime;

    private WaitForSeconds _WaitOne = new WaitForSeconds(1);

    public void Init()
    {
        _collider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = new Color(1, 1, 1, 0);
        _spriteRenderer.DOFade(1, 1);
    }

    public void Release()
    {
        _spriteRenderer.DOFade(0, 1)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    private IEnumerator ResetTornado()
    {
        yield return _WaitOne;
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Ball"))
        {
            durationTime = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Ball"))
        {
            durationTime += Time.fixedDeltaTime;
            var ball = collision.attachedRigidbody.gameObject;
            Vector2 dir = transform.position - ball.transform.position;
            float currentPower = Mathf.Clamp((_gravityPower - durationTime * 50), 0, float.MaxValue);
            ball.GetComponent<Ball>().AddForce(dir * currentPower);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Ball"))
        {
            if(_collider.enabled)
                StartCoroutine("ResetTornado");
            _collider.enabled = false;
        }
    }
}
