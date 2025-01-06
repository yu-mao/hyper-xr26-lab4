using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event Action HitABall;
    
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float projectForce = 50f;
    
    private float elapsedTime = 0f;
    private GameObject ballHit;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(projectForce * transform.forward, ForceMode.Impulse);
        gameObject.transform.parent = null;
    }
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifeTime) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out BouncyBall ball))
        {
            ballHit = other.gameObject;
            HitABall?.Invoke();
            StartCoroutine(DestroyABall());
        }
    }

    private IEnumerator DestroyABall()
    {
        yield return ballHit.transform.DOShakePosition(0.5f, 1f, 30);
        yield return new WaitForSeconds(0.5f);
        Destroy(ballHit);
    }
}
