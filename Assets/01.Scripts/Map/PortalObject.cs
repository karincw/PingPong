using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PortalObject : MonoBehaviour
{
    public PortalObject _warpTarget;
    private CircleCollider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private WaitForSeconds waitHalf = new WaitForSeconds(0.5f);

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

    public void Warp(GameObject target)
    {
        DisableWarp();
        _warpTarget.DisableWarp();
        target.transform.position = _warpTarget.transform.position;
    }

    public void DisableWarp()
    {
        _collider.enabled = false;
        StartCoroutine("EnableCoroutine");
    }

    private IEnumerator EnableCoroutine()
    {
        yield return waitHalf;
        _collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Ball"))
        {
            Warp(collision.attachedRigidbody.gameObject);
        }
    }
}
