using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUTSwitch : MonoBehaviour
{
    LUTCamera cameraFilter;
    public Material[] myLUTS = new Material[4];
    // Start is called before the first frame update
    void Start()
    {
        cameraFilter = GetComponent<LUTCamera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            cameraFilter.enabled = !cameraFilter.enabled; 
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            cameraFilter.materialRenderer = myLUTS[0]; 
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            cameraFilter.materialRenderer = myLUTS[1];
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            cameraFilter.materialRenderer = myLUTS[2];
        }
    }
}
