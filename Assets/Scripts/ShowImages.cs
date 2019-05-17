using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShowImages : MonoBehaviour
{   
    public UnityAction<bool> okularTriggerAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera")) {
            okularTriggerAction?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        okularTriggerAction?.Invoke(false);
    }


}
