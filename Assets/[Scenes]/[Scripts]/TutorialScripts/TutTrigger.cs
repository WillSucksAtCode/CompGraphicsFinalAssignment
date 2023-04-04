using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTrigger : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            TutTextAdvance.tutNum++;
            activated = true;
        }
    }
}