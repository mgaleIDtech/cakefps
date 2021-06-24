using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : MonoBehaviour
{
    public int HealthPoints { get { return _healthPoints; } }

    [SerializeField]
    protected int _healthPoints = 1;

    public void HurtActor(int hpToRemove)
    {
        if (hpToRemove < 0)
            hpToRemove = 0;

        _healthPoints -= hpToRemove;

        if (_healthPoints <= 0)
            Kill();
    }

    public void HealActor(int hpToRemove)
    {
        if (hpToRemove < 0)
            hpToRemove = 0;

        _healthPoints += hpToRemove;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
