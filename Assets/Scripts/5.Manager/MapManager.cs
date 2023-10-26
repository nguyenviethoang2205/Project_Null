using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();
    private Character character = new Character();
    private bool EncryptionEnable;
    private long saveTime;

    public void SerializeJson()
    {
        long startTime = DateTime.Now.Ticks;
        if (DataService.SaveData("/characters.json", character , EncryptionEnable))
        {
            saveTime = DateTime.Now.Ticks - startTime;
        }
        else{
            Debug.LogError("Could not save the file!");
        }
    }
}
