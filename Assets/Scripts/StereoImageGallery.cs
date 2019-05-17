using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.UI;
using Valve.VR;

[System.Serializable]
public class StereoImages
{
    public Texture2D leftEyeImage;
    public Texture2D rightEyeImage;
    public Texture2D skyImage;
    public string description;
}

[RequireComponent(typeof(AudioSource))]
public class StereoImageGallery : MonoBehaviour
{
    public GameObject _okular;

    public Material leftEyeMaterial;
    public Material rightEyeMaterial;
    public Text descriptionText;
    public Text descriptionOutText;

    public Material _entrySkyMaterial;
    public Material _defaultSkyMaterial;
    public Material _skyMaterial;

    private bool _stereoActive = true;
    private int currentImageIndex = 0;
    StereoImages currentImageSet;

    private bool isInTransition = false;

    private AudioSource _audioSource;
    public AudioClip transitionSound;
    public AudioClip glockenSound;
    private float transitionSoundDuration;

    [HideInInspector]
    public List<StereoImages> images = new List<StereoImages>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        ShowImagesByIndex(0,1);

        ChangeLayout(_stereoActive);
        transitionSoundDuration = transitionSound.length;

        RenderSettings.skybox = _entrySkyMaterial;
    }

    public bool CheckForNoImage()
    {
        //Check for no images
        if (images.Count == 0) { Debug.LogWarning("No Stereoimages found"); return false; }
        return true;
    }

    void TweenImagesForward(float start, float end, Texture2D ImageLeft, Texture2D ImageRight)
    {

        float tweenFloat = start;
        leftEyeMaterial.SetTexture("_OutViewImage", ImageLeft);
        rightEyeMaterial.SetTexture("_OutViewImage", ImageRight);

        TweenFactory.Tween(tweenFloat, start, end, 2f, TweenScaleFunctions.CubicEaseInOut, (v1) => {
            leftEyeMaterial.SetFloat("_Offset", v1.CurrentValue);
            rightEyeMaterial.SetFloat("_Offset", v1.CurrentValue);

            Vector3 textPos = descriptionText.rectTransform.anchoredPosition;
            textPos.x = -v1.CurrentValue * 1000f;
            descriptionText.rectTransform.anchoredPosition = textPos;

            Vector3 textOutPos = descriptionOutText.rectTransform.anchoredPosition;
            textOutPos.x = 1000f - (v1.CurrentValue * 1000f);
            descriptionOutText.rectTransform.anchoredPosition = textOutPos;

        }, (v2) => {
           
            leftEyeMaterial.SetTexture("_InViewImage", ImageLeft);
            rightEyeMaterial.SetTexture("_InViewImage", ImageRight);
            leftEyeMaterial.SetFloat("_Offset", 0);
            rightEyeMaterial.SetFloat("_Offset", 0);

            Text _oldText = descriptionOutText;
            descriptionOutText = descriptionText;
            descriptionText = _oldText;

            isInTransition = false;
        });
    }
    void TweenImagesBackward(float start, float end, Texture2D ImageLeft, Texture2D ImageRight)
    {
        float tweenFloat = start;

        leftEyeMaterial.SetFloat("_Offset",1);
        rightEyeMaterial.SetFloat("_Offset", 1);
        leftEyeMaterial.SetTexture("_OutViewImage", leftEyeMaterial.GetTexture("_InViewImage"));
        rightEyeMaterial.SetTexture("_OutViewImage", rightEyeMaterial.GetTexture("_InViewImage"));

        leftEyeMaterial.SetTexture("_InViewImage", ImageLeft);
        rightEyeMaterial.SetTexture("_InViewImage", ImageRight);

        TweenFactory.Tween(tweenFloat, start, end, transitionSound.length, TweenScaleFunctions.CubicEaseInOut, (v1) => {
            leftEyeMaterial.SetFloat("_Offset", v1.CurrentValue);
            rightEyeMaterial.SetFloat("_Offset", v1.CurrentValue);

            Vector3 textPos = descriptionText.rectTransform.anchoredPosition;
            textPos.x = 1000f - v1.CurrentValue * 1000f;
            descriptionText.rectTransform.anchoredPosition = textPos;

            Vector3 textOutPos = descriptionOutText.rectTransform.anchoredPosition;
            textOutPos.x = (v1.CurrentValue * -1000f);
            descriptionOutText.rectTransform.anchoredPosition = textOutPos;

        }, (v2) => {
           
            leftEyeMaterial.SetTexture("_InViewImage", ImageLeft);
            rightEyeMaterial.SetTexture("_InViewImage", ImageRight);
            leftEyeMaterial.SetFloat("_Offset", 0);
            rightEyeMaterial.SetFloat("_Offset", 0);

            Text _oldText = descriptionOutText;
            descriptionOutText = descriptionText;
            descriptionText = _oldText;

            isInTransition = false;
        });
    }

    public void ShowImagesByIndex(int index, float direction)
    {
        //play Glocken Sound to indicate an image transition
        _audioSource.clip = glockenSound;
        _audioSource.Play();

        //Set default to stereoImage
        ChangeLayout(true);

        //Change Image after glockensound
        StartCoroutine(ChangeImageByIndex(transitionSound.length, index, direction));
    }


    private IEnumerator ChangeImageByIndex(float delay, int index, float direction)
    {
        yield return new WaitForSeconds(delay);

        //Play TransitionSound
        _audioSource.clip = transitionSound;
        _audioSource.Play();

        //Image to show
        currentImageSet = images[index];
        isInTransition = true;

        descriptionOutText.text = currentImageSet.description;

        //Check for one missing image
        if (currentImageSet.leftEyeImage == null || currentImageSet.rightEyeImage == null) { Debug.LogWarning($"No Image attached! Left: {currentImageSet.leftEyeImage}, Right {currentImageSet.rightEyeImage}"); yield break; }

        if (direction > 0)
        {
            TweenImagesForward(0, 1, currentImageSet.leftEyeImage, currentImageSet.rightEyeImage);
        }
        else
        {
            TweenImagesBackward(1, 0, currentImageSet.leftEyeImage, currentImageSet.rightEyeImage);
        }

      
        if (currentImageSet.skyImage == null) { yield break; }
        _skyMaterial.mainTexture = currentImageSet.skyImage;


    }

    public void NextImage()
    {
        if (CheckForNoImage() && !isInTransition ) {

            
            //Calculate next Index
            currentImageIndex = currentImageIndex >= images.Count - 1  ? 0 : currentImageIndex + 1;

            ShowImagesByIndex(currentImageIndex, 1);
        }
    }

    public void PreviousImage()
    {
        if (CheckForNoImage() && !isInTransition)
        {
            //Calculate prev Index
            currentImageIndex = currentImageIndex >= 1 ? currentImageIndex - 1 : images.Count - 1;

            ShowImagesByIndex(currentImageIndex, -1);
        }
    }


    public void ChangeSky(bool _isInGlass)
    {
        RenderSettings.skybox = _isInGlass ? _defaultSkyMaterial : _entrySkyMaterial;
    }

    public void ChangeLayout(bool stereoActive)
    {
        _okular.SetActive(stereoActive);
        if (stereoActive)
        {
            RenderSettings.skybox = _defaultSkyMaterial;
        }
        else
        {
            RenderSettings.skybox = _skyMaterial;
        }
    }

    void Update()
    {
        if (SteamVR_Actions._default.ToggleStereoSky.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if(currentImageSet.skyImage != null) { 
                _stereoActive = !_stereoActive;
                ChangeLayout(_stereoActive);
            }
        }
    }


}
