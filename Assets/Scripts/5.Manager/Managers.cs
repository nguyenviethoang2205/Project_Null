using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]

public class Managers : MonoBehaviour
{
    private static PlayerInputManager _inputManager;

    public static PlayerInputManager Input 
    { 
        get 
        {
            return _inputManager;
        }
    }
    

    public void Awake() {
        _inputManager = GetComponent<PlayerInputManager>();
    }
}
