using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StereoImages
{
    public Texture2D leftEyeImage;
    public Texture2D rightEyeImage;
}
public class StereoImageGallery : MonoBehaviour
{
    public Material leftEyeMaterial;
    public Material rightEyeMaterial;
    public Material glassMaterial;
    private int currentImageIndex = 0;

    [HideInInspector]
    public List<StereoImages> images = new List<StereoImages>();
    
    public bool CheckForNoImage()
    {
        //Check for no images
        if (images.Count == 0) { Debug.LogWarning("No Stereoimages found"); return false; }
        return true;
    }

    public void ShowImagesByIndex(int index)
    {
        //Image to show
        StereoImages currentImageSet = images[index];

        //Check for one missing image
        if (currentImageSet.leftEyeImage == null || currentImageSet.rightEyeImage == null) { Debug.LogWarning($"No Image attached! Left: {currentImageSet.leftEyeImage}, Right {currentImageSet.rightEyeImage}"); return; }


        leftEyeMaterial.mainTexture = currentImageSet.leftEyeImage;
        rightEyeMaterial.mainTexture = currentImageSet.rightEyeImage;
        glassMaterial.SetTexture("_EmissionMap", currentImageSet.leftEyeImage);
    }

    public void NextImage()
    {
        if (CheckForNoImage()) { 

            //Calculate next Index
            currentImageIndex = currentImageIndex >= images.Count - 1  ? 0 : currentImageIndex + 1;

            ShowImagesByIndex(currentImageIndex);
        }
    }

    public void PreviousImage()
    {
        if (CheckForNoImage())
        {
            //Calculate prev Index
            currentImageIndex = currentImageIndex >= 1 ? currentImageIndex - 1 : images.Count - 1;

            ShowImagesByIndex(currentImageIndex);
        }
    }
    
}
