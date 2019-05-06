using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ToggleStereoSky : MonoBehaviour
{
    public bool _stereoActive = true;

    private StereoImageGallery _stereoImageGallery;

    private void Awake()
    {
        _stereoImageGallery = GetComponent<StereoImageGallery>();
        ChangeSetting(_stereoActive);
    }


    public void ChangeSetting(bool stereoActive)
    {
        _stereoImageGallery.ChangeLayout(stereoActive);
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
