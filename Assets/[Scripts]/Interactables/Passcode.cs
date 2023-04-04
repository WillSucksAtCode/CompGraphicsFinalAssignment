using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Passcode : MonoBehaviour
{
    string code = "130013";
    string num = null;

    [SerializeField] private Text uiText = null;
    [SerializeField] private GameObject fadeOutScreen;
    [SerializeField] private GameObject allUI;
    [SerializeField] private GameObject weaponHolder;

    private InputManager input;

    void Start()
    {
        input = InputManager.Instance;
    }

    void Update()
    {
        if (input.GetEscape())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            LockInteraction.enteringPassword = false;
            PauseMenu.inPasscode = false;
        }
    }

    public void CodeFunction(string numbers)
    {
        num += numbers;
        uiText.text = num;
    }

    public void Enter()
    {
        if (num == code)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            this.gameObject.SetActive(false);
            Time.timeScale = 1;

            // Do important stuff like start an anim
            // Correct Code
            PauseMenu.inPasscode = false;
            fadeOutScreen.SetActive(true);
            allUI.SetActive(false);
            weaponHolder.SetActive(false);
            Invoke("EndForest", 3);
        }
    }

    public void Delete()
    {
        num = null;
        uiText.text = num;
    }

    void EndForest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}