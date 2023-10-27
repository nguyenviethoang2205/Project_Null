using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{

    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("Data exist, deleting old file and writing new one");
                File.Delete(path);
            }

            else
            {
                Debug.Log("Create data fist time");
            }

            using (FileStream stream = File.Create(path))
            {
                stream.Close();
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;

        }

        catch (Exception e)
        {

            Debug.LogError($"Cant save date due to: {e.Message} {e.StackTrace}");
            return false;

        }


    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (!File.Exists(path))
        {
            Debug.LogError($"Cant not load at {path}. File dont exist");
            throw new FileNotFoundException("${path} not exist");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Cant load date due to: {e.Message} {e.StackTrace}");
            throw e;
        }

    }


}
