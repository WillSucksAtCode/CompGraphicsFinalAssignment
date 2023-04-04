using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointToggle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        this.GetComponent<CompassElementToggler>().ToggleElement(CompassElementToggler.CompassElementGroup.WAYPOINT);
    }

    void OnTriggerExit(Collider other)
    {
        this.GetComponent<CompassElement>().SetEnable(true);
    }
}