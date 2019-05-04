using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLocation : MonoBehaviour
{
    public GameObject Controller;
    public GameObject SceneObject;

    public void ResetObjectLocation()
    {
        Vector3 referenceLocation = Controller.transform.position;
        SceneObject.transform.position = new Vector3(referenceLocation.x, 0, referenceLocation.z);
    }


    private void Update()
    {
        if (Input.GetButtonDown("ResetLocation"))
        {
            if(Controller == null || !Controller.activeSelf) { Debug.LogWarning("No Controller in scene"); return; }
            ResetObjectLocation();
        }
    }


}
