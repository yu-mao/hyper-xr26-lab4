using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float projectForce = 50f;
    
    private float elapsedTime = 0f;

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
}
