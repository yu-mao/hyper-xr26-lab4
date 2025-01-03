using System.Collections;
using TMPro;
using UnityEngine;

public class ExampleGameSceneController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private BouncyBall bouncyBallPrefab;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int spawnCount;
    [SerializeField]
    private float gameDuration;
    [SerializeField]
    private TextMeshPro timeLeftLabel;

    private GameManager gameManager;
    private IInputProvider inputProvider;
    private IControllerInput leftController;
    private IControllerInput rightController;
    private float startTime;

    private void Start()
    {
        if (gameManager == null)
        {
            Initialize(GameManager.BootstrapFromEditor());
        }
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        inputProvider = gameManager.InputProvider;
        leftController = inputProvider.GetLeftController();
        rightController = inputProvider.GetRightController();

        startTime = Time.time;
        StartCoroutine(GameSequence());
    }

    private IEnumerator GameSequence()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            var ball = Instantiate(bouncyBallPrefab, new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10)), Quaternion.identity);
            ball.transform.localScale = Vector3.one * Random.Range(0.4f, 1f);
        }
    }

    private void Update()
    {
        var forward = inputProvider.GetHeadTransform().forward;
        forward.y = 0f;
        forward.Normalize();

        var right = inputProvider.GetHeadTransform().right;
        right.y = 0f;
        right.Normalize();

        forward *= rightController.Joystick.y;
        right *= rightController.Joystick.x;

        var movement = forward + right;
        movement *= movementSpeed * Time.deltaTime;

        inputProvider.GetRigTransform().position += movement;

        var elapsed = Time.time - startTime;
        var timeLeft = gameDuration - elapsed;
        if (timeLeft < 0)
        {
            gameManager.GoToMenuScene();
            timeLeftLabel.text = "Game Over";
        }
        else
        {
            timeLeftLabel.text = $"{timeLeft:0.00}";
        }
    }
}
