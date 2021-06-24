using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupBase
{
    public int HealPower = 3;
    protected override void _OnTriggerEnter(Collider other)
    {
        ActorBase actor;
        if (other.gameObject.TryGetComponent(out actor))
        {
            if (actor.CompareTag("Player"))
            {
                actor.HealActor(HealPower);
                Destroy(gameObject);
            }
        }
    }
}
