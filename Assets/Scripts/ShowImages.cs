using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImages : MonoBehaviour
{
    public GameObject okular;
    public GameObject Description;
    public GameObject ImageHolder;
    public GrabObject _grabObject;
    public StereoImageGallery _stereoImageGallery;

    public bool _isInGlass;


    private void Awake()
    {
        ImageHolder.SetActive(false);
        okular.GetComponent<MeshRenderer>().enabled = true;
        Description.SetActive(true);
    }

    public void ShowGlass(bool showGlass)
    {
        okular.GetComponent<MeshRenderer>().enabled = showGlass;
        Description.SetActive(showGlass);        
        ImageHolder.SetActive(!showGlass);

        _stereoImageGallery.ChangeSky(!showGlass);
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) {
            ShowGlass(false);

            _isInGlass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
            _isInGlass = false;
        ShowGlass(true);
    }


}
