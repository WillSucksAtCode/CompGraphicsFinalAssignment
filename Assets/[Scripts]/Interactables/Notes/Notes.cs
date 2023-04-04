using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    private InputManager input;

    //variable declaration
    [SerializeField]
    private Image _noteImage;

    public GameObject MessagePanel;

    public bool Action = false;

    private void Start()
    {
        input = InputManager.Instance;

        MessagePanel.SetActive(false);
    }

    private void Update()
    {
        if (input.GetInteract())
        {
            Debug.Log("INTERACTING");
            if (Action)
            {
                MessagePanel.SetActive(false);
                Action = false;
                _noteImage.enabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MessagePanel.SetActive(true);
            Action = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MessagePanel.SetActive(false);
            Action = false;
            _noteImage.enabled = false;
        }
    }
}
