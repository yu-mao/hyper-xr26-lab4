using UnityEngine;

public class JoystickSceneController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    private GameManager gameManager;
    private IInputProvider inputProvider;
    private IControllerInput leftController;
    private IControllerInput rightController;

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
    }

    private void Update()
    {
        if (leftController.IsButtonPressed(ControllerButtonId.Two))
        {
            gameManager.GoToMenuScene();
        }

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
}

