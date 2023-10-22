// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;

// //Singleton class
// public class DataPersistenceManager : MonoBehaviour
// {
//     private GameData gameData;
//     public static DataPersistenceManager instance { get; private set; }
//     private List<IDataPersistence> dataPersistencesObjects;
//     private void Awake()
//     {

//         if (instance != null)
//         {
//             Debug.Log("Found more than 1 Data Persistence Manager in the scene");
//         }
//         instance = this;

//     }

//     private void Start()
//     {
//         this.dataPersistencesObjects = FindAllDataPersistenceObjects();
//         LoadGame();
//     }


//     public void NewGame()
//     {
//         this.gameData = new GameData();
//     }

//     public void LoadGame()
//     {
//         //Lưu mọi dữ liệu vào FileDataHandler
//         //Nếu không có dữ liệu để load thì new game
//         if (this.gameData == null)
//         {

//             Debug.Log("Dont have any file to load");
//             NewGame();

//         }
//         //TODO - put the load data to all the script need it 
//         foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
//         {
//             dataPersistenceObj.LoadData(gameData);
//         }
//     }

//     public void SaveGame()
//     {
//         //TODO - Pass the data to other script so they can update it
//         foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects){
//             dataPersistenceObj.SaveData(ref gameData);
//         }
//         //TODO - Save that dât to a file using the dât holder!!!
//     }

//     private List<IDataPersistence> FindAllDataPersistenceObjects()
//     {
//         IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        
//         return new List<IDataPersistence>(dataPersistencesObjects);

//     }

// }
