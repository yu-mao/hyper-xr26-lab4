using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Entrypoint : MonoBehaviour
{
	[SerializeField]
	private GameManager gameManager;
    [SerializeField]
    private TextMeshPro title;

    private void Start()
    {
        StartCoroutine(PlayStartupSequence());
    }

    private IEnumerator PlayStartupSequence()
    {
        title.transform.localScale = Vector3.zero;

        yield return title.transform.DOScale(endValue: 2f, duration: 3f).WaitForCompletion();
        yield return new WaitForSeconds(1f);

        yield return title.transform.DOScale(0f, 0.3f).WaitForCompletion();
        yield return new WaitForSeconds(0.5f);

        gameManager.GoToMenuScene();
    }
}
