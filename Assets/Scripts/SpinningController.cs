using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningController : MonoBehaviour
{
    public float SpinningSpeed = 25f;

    void Update()
    {
        var rot = transform.rotation.eulerAngles;
        rot.y += SpinningSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rot);
    }
}
