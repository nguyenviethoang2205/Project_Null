
using System;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Path : MonoBehaviour
{
    [SerializeField]
    public GameObject[] zone;
    public ZoneSelect zoneSelect;
    public bool isMove{get; set;}
    private void Awake()
    {
        zoneSelect = GetComponentInChildren<ZoneSelect>();

        for (int i = 2; i <= 9; i++)
        {
            if (!zoneSelect.isCompleted) //neu chua hoan thanh thi khong hien
            {
                zone[i].SetActive(false);
            }

        }
    }

    private void Update() {
        
    }

}
