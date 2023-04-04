using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimations : MonoBehaviour
{
    public int lightMode;
    public GameObject fixtureLight1;
    public GameObject fixtureLight2;
    public GameObject fixtureLight3;

    void Update()
    {
        if (lightMode == 0)
        {
            StartCoroutine(AnimateLight());
        }
    }

    IEnumerator AnimateLight()
    {
        lightMode = Random.Range(1, 4);

        if (lightMode == 1)
        {
            fixtureLight1.GetComponent<Animation>().Play("LightAnim1");
            fixtureLight2.GetComponent<Animation>().Play("LightAnim1");
            fixtureLight3.GetComponent<Animation>().Play("LightAnim1");
        }
        if (lightMode == 2)
        {
            fixtureLight1.GetComponent<Animation>().Play("LightAnim2");
            fixtureLight2.GetComponent<Animation>().Play("LightAnim2");
            fixtureLight3.GetComponent<Animation>().Play("LightAnim2");
        }
        if (lightMode == 3)
        {
            fixtureLight1.GetComponent<Animation>().Play("LightAnim3");
            fixtureLight2.GetComponent<Animation>().Play("LightAnim3");
            fixtureLight3.GetComponent<Animation>().Play("LightAnim3");
        }
        yield return new WaitForSeconds(0.75f);
        lightMode = 0;
    }
}