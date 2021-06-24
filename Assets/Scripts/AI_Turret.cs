using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Turret : EnemyBase
{
    [SerializeField]
    private Transform _pivotPoint = null;

    [SerializeField]
    private Gun _gun = null;

    private const float SEE_DISTANCE = 20f;

    private void Update()
    {
        ShootAtPlayer();
        LookForPlayer();
    }

    private void LookForPlayer()
    {
        var player = GameManager.Instance._PlayerCharacter;

        transform.LookAt(player.transform);
    }

    private void ShootAtPlayer()
    {
        var dist = Vector3.Distance(transform.position, GameManager.Instance._PlayerCharacter.transform.position);
        if (dist <= SEE_DISTANCE)
            _gun.Shoot();
    }
}
