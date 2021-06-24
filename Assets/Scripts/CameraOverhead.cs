using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverhead : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = GameManager.Instance._PlayerCharacter.transform.position;
    }
}
