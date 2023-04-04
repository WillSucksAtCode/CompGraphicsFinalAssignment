using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CompassElementToggler : MonoBehaviour
{
    public static CompassElementToggler instance;

    private void Awake()
    {
        instance = this;
        elementToggle.Add(CompassElementGroup.NONE, null);
        elementToggle.Add(CompassElementGroup.ENEMY, new UnityEvent<bool>());
        elementToggle.Add(CompassElementGroup.WAYPOINT, new UnityEvent<bool>());
        elementState.Add(CompassElementGroup.NONE, true);
        elementState.Add(CompassElementGroup.ENEMY, true);
        elementState.Add(CompassElementGroup.WAYPOINT, true);
    }

    public enum CompassElementGroup
    {
        NONE,
        ENEMY,
        WAYPOINT
    }

    public Dictionary<CompassElementGroup, UnityEvent<bool>> elementToggle = new Dictionary<CompassElementGroup, UnityEvent<bool>>();
    public Dictionary<CompassElementGroup, bool> elementState = new Dictionary<CompassElementGroup, bool>();

    public void ToggleElement(CompassElementGroup group)
    {
        Debug.Log("Trigger");
        elementState[group] = !elementState[group];
        elementToggle[group].Invoke(elementState[group]);
    }

    public void SetToggle(bool state)
    {
        elementState[CompassElementGroup.WAYPOINT] = state;
        elementToggle[CompassElementGroup.WAYPOINT].Invoke(elementState[CompassElementGroup.WAYPOINT]);
    }

    public UnityEngine.InputSystem.InputAction deleteThis;

    private void OnEnable()
    {
        deleteThis.started += ctx => SetToggle(false);
        deleteThis.Enable();
    }
}