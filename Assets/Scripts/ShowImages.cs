using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImages : MonoBehaviour
{
    public GameObject okular;
    public GameObject ImageHolder;
    public GrabObject _grabObject;

    private void Awake()
    {
        ImageHolder.SetActive(false);
        okular.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) { 
            okular.GetComponent<MeshRenderer>().enabled = false;
            ImageHolder.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        okular.GetComponent<MeshRenderer>().enabled = true;
        ImageHolder.SetActive(false);
    }


}
