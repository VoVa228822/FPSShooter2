using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToAnotherRoom : MonoBehaviour
{
    public GameObject Light;

    public bool Change;
    private void OnTriggerExit(Collider other)
    {
        if(!Change)
        {
            Light.transform.rotation = Quaternion.Euler(70,0,0);
            Change = true;
        }
        else if(Change)
        {
            Light.transform.rotation = Quaternion.Euler(-20,0,0);
            Change = false;
        }
    }
}
