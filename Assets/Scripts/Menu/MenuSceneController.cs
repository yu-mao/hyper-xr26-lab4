using System;
using System.Collections;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    private GameManager gameManager;
    private IInputProvider inputProvider;

    private void Start()
    {
        if(gameManager == null)
        {
            Initialize(GameManager.BootstrapFromEditor());
        }
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;

        inputProvider = gameManager.InputProvider;

        inputProvider.GetRigTransform().position = Vector3.zero;

        StartCoroutine(PlayStartupSequence());
    }

    private IEnumerator PlayStartupSequence()
    {
        yield return gameManager.LoadingScreen.Hide();
    }

    private void Update()
    {
        if (inputProvider.GetLeftController().IsButtonPressed(ControllerButtonId.One))
        {
            gameManager.GoToJoystickScene();
        }
        if (inputProvider.GetRightController().IsButtonPressed(ControllerButtonId.One))
        {
            gameManager.GoToTeleportScene();
        }
        if (inputProvider.GetRightController().IsButtonPressed(ControllerButtonId.Two))
        {
            gameManager.GoToClimbingScene();
        }
    }
}

