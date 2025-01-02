using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public void Initialize(Transform headTransform)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetFloat("_Alpha", 0f);
        transform.SetParent(headTransform);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    public IEnumerator Show()
    {
        if (gameObject.activeSelf)
        {
            //No need to show again if already visible
            yield break;
        }

        meshRenderer.material.SetFloat("_Alpha", 0f);
        gameObject.SetActive(true);
        yield return meshRenderer.material.DOFloat(1f, "_Alpha", 1f).WaitForCompletion();
    }

    public IEnumerator Hide()
    {
        if(!gameObject.activeSelf)
        {
            //No need to hide again if not visible
            yield break;
        }

        meshRenderer.material.SetFloat("_Alpha", 1f);
        yield return meshRenderer.material.DOFloat(0f, "_Alpha", 1f).WaitForCompletion();
        Debug.Log("done");
        gameObject.SetActive(false);
    }
}
