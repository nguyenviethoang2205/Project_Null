using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class NewData : MonoBehaviour
{
    float[] position;
    bool[] status;

    #region Data
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    #endregion

    bool[] GetContain(){
        status = new bool[10];
        for (int i = 0; i < 10; i++)
        {
            status[i] = false;
        }
        return status;
    }
    
    public float[] GetPos()
    {
        position = new float[3];
        position[0] = (float) 0.0;
        position[1] = (float) -15.5;
        position[2] = (float) 0.0;
        return position;
    }

    public void NewStatus()
    {
        GetContain();
        JsonConvert.SerializeObject(status, Formatting.Indented,
            new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        if (DataService.SaveData("/status.json", status, EncryptionEnable))
        {

            Debug.Log(status);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }

    public void NewPosition(){
        GetPos();
        JsonConvert.SerializeObject(position, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

        if (DataService.SaveData("/position.json", position, EncryptionEnable))
        {

            Debug.Log(position);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }
}
