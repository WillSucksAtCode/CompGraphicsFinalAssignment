using UnityEngine;

public class CompassElement : MonoBehaviour
{
    [Tooltip("The marker on the compass for this element")]
    public CompassMarker CompassMarkerPrefab;

    [Tooltip("Text override for the marker, if it's a direction")]
    public string TextDirection;

    Compass m_Compass;

    CompassMarker m_Marker;

    [SerializeField] CompassElementToggler.CompassElementGroup m_Group;

    void Awake()
    {
        m_Compass = FindObjectOfType<Compass>();

        m_Marker = Instantiate(CompassMarkerPrefab);

        m_Marker.Initialize(this, TextDirection);
        m_Compass.RegisterCompassElement(transform, m_Marker);
    }

    private void Start()
    {
        CompassElementToggler.instance.elementToggle[m_Group]?.AddListener(SetEnable);
        if (CompassElementToggler.instance.elementState[m_Group] == false)
        {
            SetEnable(false);
        }
    }

    void OnDestroy()
    {
        m_Compass.UnregisterCompassElement(transform);
        CompassElementToggler.instance.elementToggle[m_Group]?.RemoveListener(SetEnable);
    }

    public void SetEnable(bool state)
    {
        m_Marker.gameObject.SetActive(state);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetEnable(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetEnable(true);
        }
    }
}