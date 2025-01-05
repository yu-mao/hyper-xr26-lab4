using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    private GameManager gameManager;
    private IInputProvider inputProvider;
    private IControllerInput leftController;
    private IControllerInput rightController;
    
    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
        inputProvider = gameManager.InputProvider;
        leftController = inputProvider.GetLeftController();
        rightController = inputProvider.GetRightController();
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
        
    }
}
