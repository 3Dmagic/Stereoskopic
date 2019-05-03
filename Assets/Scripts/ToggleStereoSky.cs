using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ToggleStereoSky : MonoBehaviour
{
    public bool _stereoActive = true;

    [SerializeField]
    private Material _defaultSkyMat;
    [SerializeField]
    private Material _gallerySkyMat;
    [SerializeField]
    private GameObject _stereoObjects;

    private void Awake()
    {
        ChangeSetting(_stereoActive);
    }


    public void ChangeSetting(bool stereoActive)
    {
        if (stereoActive)
        {
            RenderSettings.skybox = _defaultSkyMat;
            _stereoObjects.SetActive(true);
        }
        else
        {
            RenderSettings.skybox = _gallerySkyMat;
            _stereoObjects.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.ToggleStereoSky.GetStateDown(SteamVR_Input_Sources.Any))
        {
            _stereoActive = !_stereoActive;

            ChangeSetting(_stereoActive);
        }
    }
}
