using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObject : MonoBehaviour
{
    bool isInTrigger = false;
    public bool grabbed = false;
    private GameObject controller;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameController")) { 
            isInTrigger = true;
            controller = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        controller = null;
    }
    

    void AttachObject()
    {
        if (isInTrigger)
        {
            transform.parent = controller.transform;
            grabbed = true;
        }
    }

    void DetachObject()
    {
        transform.parent = null;
        grabbed = false;
    }

    private void Update()
    {
        if (SteamVR_Actions._default.GrabObject.GetStateDown(SteamVR_Input_Sources.Any))
        {
            AttachObject();
        }

        if (SteamVR_Actions._default.GrabObject.GetStateUp(SteamVR_Input_Sources.Any))
        {
            DetachObject();
        }
    }
}
