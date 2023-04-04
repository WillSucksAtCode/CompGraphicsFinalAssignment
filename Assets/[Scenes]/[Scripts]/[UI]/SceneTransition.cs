using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadCredits()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 4));
    }

    public void LoadTutorial()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 3));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
