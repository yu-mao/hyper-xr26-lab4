using System;
using System.Collections;
using UnityEngine;

public class Entrypoint : MonoBehaviour
{
	[SerializeField]
	private GameManager gameManager;

    private void Start()
    {
        gameManager.GoToMenuScene();
    }
}
