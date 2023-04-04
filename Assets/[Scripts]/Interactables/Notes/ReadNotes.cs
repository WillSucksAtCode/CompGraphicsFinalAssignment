using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReadNotes : MonoBehaviour
{
    //Variable Declaration 
    public GameObject _player;
    public GameObject _noteUI;
    public GameObject _hud;
    public GameObject _pickupText;

    public AudioSource _pickupSound;

    InputManager input;

    public bool inReach;
    private void Awake()
    {
        input = InputManager.Instance;
    }
    void Start()
    {
        _noteUI.SetActive(false);
        _hud.SetActive(true);
        _pickupText.SetActive(false);

        inReach = false;
    }

    private void Update()
    {
        if (input.GetInteract())
        {
            if (inReach)
            {
                _noteUI.SetActive(true);
                _pickupSound.Play();
                _hud.SetActive(false);
                _player.GetComponent<FirstPersonController>().enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void ExitButton()
    {
        _noteUI.SetActive(false);
        _hud.SetActive(true);
        _player.GetComponent<FirstPersonController>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = true;
            _pickupText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inReach = false;
            _pickupText.SetActive(false);
        }
    }

}
