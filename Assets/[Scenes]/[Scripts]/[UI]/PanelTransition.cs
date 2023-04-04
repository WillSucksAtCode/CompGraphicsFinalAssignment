using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelTransition : MonoBehaviour
{
    public GameObject panel;

    public Animator panelTransition;

    public float panelTransitionTime = 1f;

    public void LoadTransition(bool buttonPressed)
    {
        panelTransition.SetBool("ButtonPressed", buttonPressed);
    }
}
