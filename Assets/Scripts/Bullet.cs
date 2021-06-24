using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    private void Update()
    {
        var moveVector = transform.TransformDirection(Vector3.forward) * (_speed * Time.deltaTime);
        //_cc.Move(moveVector);
    }
}
