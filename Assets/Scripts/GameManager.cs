using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private VRInputManager vrInputManagerPrefab;
    [SerializeField]
    private KeyboardMouseInputManager keyboardMouseInputManager;
    [SerializeField]
    private bool useKeyboardMouse = false;
    [SerializeField]
    private LoadingScreen loadingScreen;

    public IInputProvider InputProvider { get; private set; }
    public LoadingScreen LoadingScreen => loadingScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if !UNITY_EDITOR
		useKeyboardMouse = false;
#endif
        if (useKeyboardMouse)
        {
            InputProvider = Instantiate(keyboardMouseInputManager, transform);
        }
        else
        {
            InputProvider = Instantiate(vrInputManagerPrefab, transform);
        }

        InputProvider.TryInitialize();
        loadingScreen.Initialize(InputProvider.GetHeadTransform());
    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            StartCoroutine(loadingScreen.Show());
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            StartCoroutine(loadingScreen.Hide());
        }
    }

    public void GoToJoystickScene()
    {
        StartCoroutine(LoadScene("Game_Joystick", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<JoystickSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public void GoToClimbingScene()
    {
        throw new NotImplementedException();
    }

    public void GoToTeleportScene()
    {
        throw new NotImplementedException();
    }

    public void GoToMenuScene()
    {
        StartCoroutine(LoadScene("Menu", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<MenuSceneController>();
            sceneController.Initialize(this);
        }));
    }

    private IEnumerator LoadScene(string sceneName, Action sceneLoadedCallback)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        sceneLoadedCallback?.Invoke();
    }

    public static GameManager BootstrapFromEditor()
    {
#if UNITY_EDITOR
        var gameManagerPrefab = Resources.Load<GameManager>("GameManager");
        var gameManager = Instantiate(gameManagerPrefab);
        return gameManager;
#else
        throw new NotImplementedException();
#endif
    }
}

