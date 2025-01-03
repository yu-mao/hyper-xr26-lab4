using DG.Tweening;
using System;
using UnityEngine;

public class RaycastTargetButton : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    public event Action Clicked;

    private Color originalColor;
    
    private void Start()
    {
        originalColor = meshRenderer.material.color;
    }

    public void OnHoverEnter()
    {
        transform.DOScale(Vector3.one * 1.1f, 0.2f);
        meshRenderer.material.DOColor(Color.yellow, 0.2f);
    }

    public void OnHoverExit() 
    {
        transform.DOScale(Vector3.one, 0.2f);
        meshRenderer.material.DOColor(originalColor, 0.2f);
    }

    public void OnClicked()
    {
        transform.DOShakePosition(0.3f, 0.1f, 15);
        Clicked?.Invoke();
    }
}
