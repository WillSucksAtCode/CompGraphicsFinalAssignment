using UnityEngine.SceneManagement;
using UnityEngine;

public class DevTools : MonoBehaviour
{
    private static DevTools _instance;

    DeveloperTools _devTools;

    bool DevToolEnabled = false;
    bool isPaused = false;
    public static DevTools Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _devTools = new DeveloperTools();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        DevToolsEnabled();

        if (DevToolEnabled)
        {
            SceneManagement();
            PauseGame();
        }
    }
    private void OnEnable()
    {
        _devTools.Enable();
    }

    private void OnDisable()
    {
        _devTools.Disable();
    }

    //This function takes in the input from the input map and enables or disables the developer tools
    private void DevToolsEnabled()
    {
        if (_devTools.General.EnableDevModeButton1.triggered && !DevToolEnabled)
        {
            DevToolEnabled = true;

            Debug.LogWarning("The Developer Tools Have Been Enabled");
        }
        else if (_devTools.General.EnableDevModeButton1.triggered && DevToolEnabled)
        {
            DevToolEnabled = false;

            Debug.LogWarning("The Developer Tools Have Been Disabled");
        }
    }

    //This function takes in the input from the input map and goes forward or back a scene when developer tools are enabled
    private void SceneManagement()
    {
        if (_devTools.SceneManagement.GoBackAScene.triggered)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if (_devTools.SceneManagement.GoForwardAScene.triggered)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //This function takes in the input from the input map and pauses the game when developer tools are enabled
    private void PauseGame()
    {
        if (_devTools.SceneManagement.PauseScene.triggered && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            Debug.Log("The Game Has Been Paused");
        }
        else if (_devTools.SceneManagement.PauseScene.triggered && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            Debug.Log("The Game Has Been Unpaused");
        }
    }
}
