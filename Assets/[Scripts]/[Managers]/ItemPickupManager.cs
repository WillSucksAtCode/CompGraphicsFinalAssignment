using System.Collections;
using UnityEngine;
using TMPro;

public class ItemPickupManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PickupText = default;

    public void CollectMed()
    {
        StopAllCoroutines();
        PickupText.text = "+1 Medpack";
        StartCoroutine(ClearText());
    }

    public void CollectAmmo(int amount, string type)
    {
        StopAllCoroutines();
        PickupText.text = $"+{amount} {type} Ammo";
        StartCoroutine(ClearText());
    }

    public void CollectKeycard()
    {
        StopAllCoroutines();
        PickupText.text = "+1 Keycard";
        StartCoroutine(ClearText());
    }

    public IEnumerator ClearText()
    {
        yield return new WaitForSeconds(2f);
        PickupText.text = "";
    }
}