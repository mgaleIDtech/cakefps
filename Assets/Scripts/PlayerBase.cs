using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : ActorBase
{
    private void Start()
    {
        GameManager.Instance._PlayerCharacter = this;
    }
}
