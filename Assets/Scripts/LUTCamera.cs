using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUTCamera : MonoBehaviour
{
    
    public Material materialRenderer;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, materialRenderer);
    }
}
