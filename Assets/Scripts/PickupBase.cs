using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        _OnTriggerEnter(other);
    }

    protected virtual void _OnTriggerEnter(Collider other)
    {
        
    }
}
