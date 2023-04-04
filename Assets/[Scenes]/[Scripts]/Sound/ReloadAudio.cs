using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAudio : MonoBehaviour
{

    [SerializeField]
    private FMODUnity.EventReference _Reload;
    private FMOD.Studio.EventInstance reload;

    public GameObject gunLocation;
    private GameObject playerCamera;

    private string gunTag;

    private void Awake()
    {
        if (!_Reload.IsNull)
        {
            reload = FMODUnity.RuntimeManager.CreateInstance(_Reload);
        }

        playerCamera = gunLocation.GetComponent<GameObject>();
    }

    public void PlayReload()
    {
        if (reload.isValid())
        {
            reload.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

            ReloadSwitch();
            reload.start();
        }
    }

    private void ReloadSwitch()
    {
        Debug.Log(gunTag);
                if (gunTag == "Reload/MAG")
                {
                    reload.setParameterByName("ReloadType", 0);
                }

                if (gunTag == "Reload/SHOTGUN")
                {
                    reload.setParameterByName("ReloadType", 1);

                }

    }

    public void ReloadType(string theTag)
    {
        gunTag = theTag;
    }
}
