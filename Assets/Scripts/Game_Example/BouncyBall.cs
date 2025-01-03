using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    [SerializeField]
    private float delayLow;
    [SerializeField]
    private float delayHigh;
    [SerializeField]
    private float impulseLow;
    [SerializeField]
    private float impulseHigh;

    private Color originalColor;
    private Rigidbody rigidBody;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();
        
        originalColor = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.9f, 1f);
        meshRenderer.material.color = originalColor;

        StartCoroutine(RandomJumpSequence());
    }

    private IEnumerator RandomJumpSequence()
    {
        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(delayLow, delayHigh));

            yield return meshRenderer.material
                .DOColor(Color.white, 0.3f)
                .SetEase(Ease.InOutFlash)
                .SetLoops(3)
                .WaitForCompletion();

            meshRenderer.material.DOColor(originalColor, 0.4f);

            var impulse = Random.Range(impulseLow, impulseHigh);
            var direction = (Random.insideUnitSphere + Vector3.up).normalized;
            rigidBody.AddForce(direction * impulse, ForceMode.Impulse);
        }
    }

}
