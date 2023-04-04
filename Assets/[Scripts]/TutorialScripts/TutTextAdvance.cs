using UnityEngine;
using TMPro;

public class TutTextAdvance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutText = default;
    public static int tutNum = 1;

    void Update()
    {
        if (tutNum == 1)
            tutText.text = "Use WASD to move and 'Shift' to sprint";

        else if (tutNum == 2)
            tutText.text = "Press 'Space' to jump and 'Ctrl' to crouch";

        else if (tutNum == 3)
            tutText.text = "Press 'E' to interact with items";

        else if (tutNum == 4)
            tutText.text = "Press 'R' to reload your weapon";

        else if (tutNum == 5)
            tutText.text = "Use '1, 2, 3' or the Mouse Wheel to change weapons";

        else if (tutNum == 6)
            tutText.text = "Use the 'Left Mouse' button to shoot";

        else if (tutNum == 7)
            tutText.text = "Press 'F' to heal if damaged";
    }
}