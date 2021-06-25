using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PressurePlate : MonoBehaviour
{
    // A pressure plate that goes down when an actor or box is on top of it.
    // 1. Tell me when an actor or box touches you.
    // 2. Change color to red when they are on you.
    // 3. Tell me when they leave you.
    // 4. Change color to white when they leave you.

    public Renderer _r = null;

    Color _defaultColor = Color.white;

    private void Awake()
    {
        _defaultColor = _r.material.GetColor("_Color");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ChangeColor(Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _ChangeColor(_defaultColor);
        }
    }

    private void _ChangeColor(Color c)
    {
        _r.material.SetColor("_Color", c);
    }
}
