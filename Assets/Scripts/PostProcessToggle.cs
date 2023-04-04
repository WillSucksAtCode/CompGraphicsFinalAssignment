using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessToggle : MonoBehaviour
{
    public GameObject postProcessing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (postProcessing.activeInHierarchy)
            {
                postProcessing.SetActive(false);
            }
            else
            postProcessing.SetActive(true);
        }
    }
}
