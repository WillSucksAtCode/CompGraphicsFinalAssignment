using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] 
    private FMODUnity.EventReference _footsteps;
    private FMOD.Studio.EventInstance footsteps;

    public GameObject cameraLocation;
    private Camera playerCamera;
    float stepType;

    private void Awake()
    {
        if (!_footsteps.IsNull)
        {
            footsteps = FMODUnity.RuntimeManager.CreateInstance(_footsteps);
        }

        playerCamera = cameraLocation.GetComponent<Camera>();
    }

    public void PlayFootsteps()
    {
        if (footsteps.isValid())
        {
            footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            GroundSwitch();
            footsteps.start();
        }
    }

    private void GroundSwitch()
    {
        //Debug.Log("Im Looking");
        if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 7))
        {
            //Debug.Log("I See Something");
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
               // Debug.Log("It's " + surfaceRenderer.gameObject.tag);
                if (surfaceRenderer.gameObject.tag == "Footsteps/CARPET")
                {
                    footsteps.setParameterByName("Footsteps", 0);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }

                if (surfaceRenderer.gameObject.tag == "Footsteps/GRAVEL")
                {
                    footsteps.setParameterByName("Footsteps", 1);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }

                if (surfaceRenderer.gameObject.tag == "Footsteps/LEAF")
                {
                    footsteps.setParameterByName("Footsteps", 2);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }

                if (surfaceRenderer.gameObject.tag == "Footsteps/WET")
                {
                    footsteps.setParameterByName("Footsteps", 3);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }

                if (surfaceRenderer.gameObject.tag == "Footsteps/STONE")
                {
                    footsteps.setParameterByName("Footsteps", 4);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }

                if (surfaceRenderer.gameObject.tag == "Footsteps/TILE")
                {
                    footsteps.setParameterByName("Footsteps", 5);
                    footsteps.getParameterByName("Footsteps", out stepType);
                }
                //Debug.Log(stepType);
            }
        }
    }
}
