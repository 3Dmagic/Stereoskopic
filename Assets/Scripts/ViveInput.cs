using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ViveInput : MonoBehaviour
{
    public SteamVR_Action_Vector2 trackpad;

    StereoImageGallery _stereoImageGallery;
    SkyGallery _skyGallery;


    private void Awake()
    {
        _stereoImageGallery = GetComponent<StereoImageGallery>();
    }

    private void Next()
    {
        _stereoImageGallery.NextImage();
    }
    private void Prev()
    {
        _stereoImageGallery.PreviousImage();       
    }


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
