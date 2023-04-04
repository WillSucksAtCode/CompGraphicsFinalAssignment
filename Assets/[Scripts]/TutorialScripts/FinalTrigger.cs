using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;
    [SerializeField] private GameObject allUI;
    [SerializeField] private GameObject weaponHolder;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fadeOut.SetActive(true);
            weaponHolder.SetActive(false);
            allUI.SetActive(false);
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}