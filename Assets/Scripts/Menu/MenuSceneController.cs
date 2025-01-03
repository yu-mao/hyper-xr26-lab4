using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private LaserPointer laserPointer;
    [SerializeField]
    private RaycastTargetButton gameButton;
    [SerializeField]
    private RaycastTargetButton exampleButton;

    private GameManager gameManager;
    private IInputProvider inputProvider;

    private void Start()
    {
        if(gameManager == null)
        {
            Initialize(GameManager.BootstrapFromEditor());
        }

        gameButton.Clicked += OnGameClicked;
        exampleButton.Clicked += OnExampleClicked;
    }

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;

        inputProvider = gameManager.InputProvider;
        inputProvider.GetRigTransform().position = Vector3.zero;

        laserPointer.Initialize(inputProvider.GetRightController());
    }

    private void OnGameClicked()
    {
        gameManager.GoToGameScene();
    }

    private void OnExampleClicked()
    {
        gameManager.GoToGameExampleScene();
    }
}

