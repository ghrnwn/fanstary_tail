using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeChanger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Entity>())
        {
            other.GetComponent<Entity>().GroundSlopeChecker();            
        }
    }
}
