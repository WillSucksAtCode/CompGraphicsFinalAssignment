using UnityEngine;
using System.Collections;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    private InputManager input;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        input = InputManager.Instance;
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // Select weapon using scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        // Select weapon using keys
        if (input.GetSwitchWeaponOne())
            selectedWeapon = 0;

        if (input.GetSwitchWeaponTwo())
            selectedWeapon = 1;

        if (input.GetSwitchWeaponThree())
            selectedWeapon = 2;

        // Switch weapon Gamepad
        if (input.GetSwitchGamepad())
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        // If selected weapon isnt active, switch
        if (previousSelectedWeapon != selectedWeapon)
            //SelectWeapon();
            StartCoroutine(SwitchWeapon());
    }

    void SelectWeapon()
    {
        int i = 0;

        // Check what weapon (gameobject) is active
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }

    IEnumerator SwitchWeapon()
    {
        animator.SetBool("Switching", true);

        yield return new WaitForSeconds(0.4f);
        int i = 0;

        animator.SetBool("Switching", false);
        // Check what weapon (gameobject) is active
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }
}