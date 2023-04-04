using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameTransition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(53);

        SceneManager.LoadScene("Outside Terrain");
    }
}
