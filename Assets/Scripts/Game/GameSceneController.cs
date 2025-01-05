using System.Collections;
using TMPro;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private int spawnCount = 20;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private BouncyBall myBouncyBallPrefab;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float gameDuration = 60f;
    [SerializeField] private TextMeshPro timeLeftLabel;
    [SerializeField] private TextMeshPro scoreLabel;
    
    private GameManager gameManager;
    private IInputProvider inputProvider;
    private IControllerInput leftController;
    private IControllerInput rightController;
    private float startTime;
    
    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        inputProvider = gameManager.InputProvider;
        leftController = inputProvider.GetLeftController();
        rightController = inputProvider.GetRightController();

        startTime = Time.time;
        StartCoroutine(GameSequence());
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (gameManager == null)
        {
            Initialize(GameManager.BootstrapFromEditor());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ApplyJoystickMovement();
        
        float elapsedTime = Time.time - startTime;
        float timeLeft = gameDuration - elapsedTime;
        if (timeLeft <= 0)
        {
            gameManager.GoToMenuScene();
            timeLeftLabel.text = "Game Over";
        }
        else
        {
            timeLeftLabel.text = "Time Left: " + timeLeft.ToString("0.00");
        }
    }

    private void ApplyJoystickMovement()
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
    }

    private IEnumerator GameSequence()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            yield return new WaitForSeconds(spawnDelay);
            
            var ball = Instantiate(myBouncyBallPrefab,
                new Vector3(Random.Range(-10f, 10f), 10f, Random.Range(-10f, 10f)), Quaternion.identity);

            if (i % 5 == 0)
            {
                ball.transform.localScale = Vector3.one * Random.Range(2.4f, 3f);
            }
            else
            {
                ball.transform.localScale = Vector3.one * Random.Range(0.4f, 1f);
            }
        }
    }
}
