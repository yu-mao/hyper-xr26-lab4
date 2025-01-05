using DG.Tweening;
using System;
using System.Collections;
using TMPro;
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

    private bool inTransition = false;

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

    public Coroutine GoToGameScene()
    {
        return StartCoroutine(LoadScene("Game", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<GameSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToGameExampleScene()
    {
        return StartCoroutine(LoadScene("Game_Example", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<ExampleGameSceneController>();
            sceneController.Initialize(this);
        }));
    }

    public Coroutine GoToMenuScene()
    {
        return StartCoroutine(LoadScene("Menu", () =>
        {
            var sceneController = GameObject.FindAnyObjectByType<MenuSceneController>();
            sceneController.Initialize(this);
        }));
    }

    private IEnumerator LoadScene(string sceneName, Action sceneLoadedCallback)
    {
        if(inTransition)
        {
            yield break;//Exit!
        }

        inTransition = true;
        yield return loadingScreen.Show();

        yield return SceneManager.LoadSceneAsync(sceneName);
        sceneLoadedCallback?.Invoke();

        yield return loadingScreen.Hide();
        inTransition = false;
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

