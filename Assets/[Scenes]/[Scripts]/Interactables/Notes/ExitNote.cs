using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitNote : MonoBehaviour
{
    [SerializeField] private GameObject noteImage;

    public void Resume()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        noteImage.SetActive(false);
    }
}