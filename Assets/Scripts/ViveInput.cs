using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ViveInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 trackpad;

    ToggleStereoSky _toggleStereoSky;

    StereoImageGallery _stereoImageGallery;
    SkyGallery _skyGallery;


    private void Awake()
    {
        _toggleStereoSky = GetComponent<ToggleStereoSky>();

        _stereoImageGallery = GetComponent<StereoImageGallery>();
       // _skyGallery = GetComponent<SkyGallery>();
    }

    private void Next()
    {
            _stereoImageGallery.NextImage();
        //if (_toggleStereoSky._stereoActive)
        //{
        //}
        //else
        //{
        //    _skyGallery.NextImage();
        //}
    }
    private void Prev()
    {
            _stereoImageGallery.PreviousImage();
        //if (_toggleStereoSky._stereoActive)
        //{
        //}
        //else
        //{
        //    _skyGallery.PreviousImage();
        //}

    }

    // Update is called once per frame
    void Update()
    {

        if (SteamVR_Actions._default.TouchDown.GetStateDown(SteamVR_Input_Sources.Any))
        {

            Vector2 menuPosition = trackpad.GetAxis(SteamVR_Input_Sources.Any);            

            if(menuPosition.x > 0)
            {
                Next();
            }
            else
            {
                Prev();
            }

        }
    }

}
