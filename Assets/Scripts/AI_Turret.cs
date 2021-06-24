using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Turret : MonoBehaviour
{
    [SerializeField]
    private Transform _pivotPoint = null;

    private void Update()
    {
        var player = GameManager.Instance._PlayerMotor;

        transform.LookAt(player.transform);
    }
}
