using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool confirmScreen = false;
    public static bool inPasscode = false;

    public GameObject pauseMenuUI;

    public GameObject confirmationUI;

    public GameObject pauseButtons;
    public GameObject optionsButtons;

    private InputManager input;

    private bool quitGame;

    void Start()
    {
        input = InputManager.Instance;
    }

    void Update()
    {
        if (input.GetEscape())
        {
            if (GameIsPaused && !confirmScreen)
                Resume();
            else if (!GameIsPaused && !inPasscode)
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        //InputManager.Instance.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionsButtons.SetActive(false);
        pauseButtons.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        //InputManager.Instance.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Viral Destruction Menu");
    }

    public void LoadConfirmation(bool quit)
    {
        confirmationUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        confirmScreen = true;
        quitGame = quit;
    }

    public void disableConfirmation()
    {
        confirmationUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        confirmScreen = false;
    }

    public void QuitGame()
    {
        confirmScreen = false;
        if (quitGame)
        {
            Application.Quit();
        }
        else
        {
            LoadMenu();
        }
    }
}
