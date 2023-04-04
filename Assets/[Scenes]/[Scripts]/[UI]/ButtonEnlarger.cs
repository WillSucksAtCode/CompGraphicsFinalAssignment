using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEnlarger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    [SerializeField]
    private float scaleMultiplier = 1.2f;

    [SerializeField]
    private float scaleDuration = 0.2f;

    private bool isHovering;

    private void Awake()
    {
        originalScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * scaleMultiplier, scaleDuration * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleDuration * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
