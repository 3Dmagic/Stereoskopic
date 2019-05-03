using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyGallery : MonoBehaviour
{
    public Material skyMaterial;
    public List<Texture2D> skyTextures = new List<Texture2D>();

    private int currentSkyIndex = 0;

    private void Awake()
    {
        SetSkyByIndex(currentSkyIndex);
    }

    public void SetSkyByIndex(int index)
    {
        if(skyTextures[index] == null) { Debug.LogWarning($"No texture applied at index: {index}"); return; }


        skyMaterial.mainTexture = skyTextures[index];
    }
    public void PreviousImage()
    {
        currentSkyIndex = currentSkyIndex >= 1 ? currentSkyIndex - 1 : skyTextures.Count - 1;
        SetSkyByIndex(currentSkyIndex);
    }
    public void NextImage()
    {
        currentSkyIndex = currentSkyIndex + 1 < skyTextures.Count ? currentSkyIndex + 1 : 0;
        SetSkyByIndex(currentSkyIndex);
    }
}
