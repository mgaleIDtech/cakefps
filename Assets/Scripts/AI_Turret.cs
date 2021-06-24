using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Turret : EnemyBase
{
    [SerializeField]
    private Transform _pivotPoint = null;

    private void Update()
    {
        var player = GameManager.Instance._PlayerCharacter;

        transform.LookAt(player.transform);
    }
}
