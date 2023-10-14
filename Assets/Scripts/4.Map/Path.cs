using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Zone[] zoneNode;

    public GameObject player;
    public float moveSpeed;
    
    private void Start() {
        zoneNode = GetComponentsInChildren<Zone>();
    }
}
