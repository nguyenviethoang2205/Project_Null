
using System;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Path : MonoBehaviour
{
    [SerializeField] 
    public GameObject[] zone;
    public GameObject currentZone;
    public ZoneSelect zoneSelect;

    private void Awake() {
        zoneSelect = GetComponentInChildren<ZoneSelect>();
    }

    private void Start() {
        
        for (int i = 2; i <= 9; i++){
            zone[i].SetActive(false);
        }
    }

    private void Update() {
    }

    
    
}
