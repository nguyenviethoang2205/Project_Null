
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class Path : MonoBehaviour
{
    #region Data
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    #endregion


    public GameObject[] zone;
    public ZoneSelect[] zoneSelect;
    private bool[] contain;
    public bool isMove { get; set; }
    public bool isPause = false;

    private void Start()
    {
        for (int i = 0; i <= 9; i++)
        {
            zoneSelect[i] = zone[i].GetComponent<ZoneSelect>();
        }

        LoadStatus();

        // for (int i = 2; i <= 9; i++)
        // {
        //     if (!zoneSelect[i].isCompleted)
        //     {
        //         zone[i].SetActive(false);
        //     }

        // }
        for (int i = 0; i <= 9; i++)
        {
            zoneSelect[i].currentZone = zoneSelect[i].selectionZone.name;
        }

    }

    public bool[] GetContain()
    {
        contain = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            contain[i] = zoneSelect[i].isCompleted;
        }
        return contain;

    }

    public void SaveStatus()
    {
        GetContain();
        JsonConvert.SerializeObject(contain, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        if (DataService.SaveData("/status.json", contain, EncryptionEnable))
        {

        }
        else
        {
            Debug.LogError("Could not save the file!");
        }

    }

    public void LoadStatus()
    {
        for (int i = 0; i <= 9; i++)
        {
            bool[] statusData = DataService.LoadData<bool[]>("/status.json", EncryptionEnable);
            zoneSelect[i].isCompleted = statusData[i];
        }

    }


    

}
