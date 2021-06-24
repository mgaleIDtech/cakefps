using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    public PlayerBase _PlayerCharacter = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("You put multiple GameManager instances in your scene.  Make sure there is only one.");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {

    }
}
