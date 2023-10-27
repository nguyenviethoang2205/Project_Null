using System.Collections;
using Cinemachine;
using DG.Tweening;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[JsonObject(MemberSerialization.OptIn)]
public class ZoneSelect : MonoBehaviour
{
    [SerializeField] private static GameObject player;
    [SerializeField] private GameObject uncompleteOj;
    [SerializeField] private GameObject selectionZone;
    [JsonProperty] private float[] currentPos;
    [JsonProperty] public bool isCompleted; //false

    public new Collider collider;
    public Path path;

    #region Data
    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnable;
    #endregion


    private void Awake()
    {
        this.selectionZone = this.gameObject;
        LoadChar();
        path = GetComponentInParent<Path>();
        LoadPos();

    }

    private void Update()
    {
        UpdateZone();
        ZoneSelected();
    }

    private void OnMouseDown()
    {
        if (path.isMove == false)
        {
            path.isMove = true;
            Move();

        }
    }
    public void UpdateZone()
    {
        switch (isCompleted)
        {
            case false:
                uncompleteOj.SetActive(true);
                break;

            case true:
                uncompleteOj.SetActive(false);
                collider.enabled = false;
                break;

        }

    }
    public IEnumerator Completed()
    {
        yield return new WaitForSeconds(1);
        isCompleted = true;
        path.SaveStatus();
        CurrentPos(player);
        Debug.Log(player.transform.position);
        SavePos();
    }

    public void SavePos()
    {
        JsonConvert.SerializeObject(currentPos, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

        if (DataService.SaveData("/position.json", currentPos, EncryptionEnable))
        {

            Debug.Log(currentPos);
        }
        else
        {
            Debug.LogError("Could not save the file!");
        }
    }

    public void LoadChar()
    {
        Character charData = DataService.LoadData<Character>("/characters.json", EncryptionEnable);

        if (player == null)
        {
            player = Instantiate(Resources.Load("Prefabs/Player/" + charData.name, typeof(GameObject))) as GameObject;
        }
    }

    public void LoadPos()
    {
        float[] posData = DataService.LoadData<float[]>("/position.json", EncryptionEnable);
        Vector3 position;
        position.x = posData[0];
        position.y = posData[1];
        position.z = posData[2];
        player.transform.position = position;
    }

    public float[] CurrentPos(GameObject _player)
    {
        currentPos = new float[3];
        currentPos[0] = _player.transform.position.x;
        currentPos[1] = _player.transform.position.y;
        currentPos[2] = _player.transform.position.z;
        return currentPos;
    }
    public void Move()
    {
        
        player.transform.DOMove(selectionZone.transform.position, 1);
        StartCoroutine(Completed());
        StartCoroutine(ZoneSelected());

    }

    public IEnumerator ZoneSelected()
    {
        yield return new WaitForSeconds(1);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        switch (selectionZone.name)
        {

            case "Zone_1":
                path.zone[2].SetActive(true);
                path.zone[3].SetActive(true);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_2":
                path.zone[4].SetActive(true);
                path.zone[3].SetActive(false);
                // SceneManager.LoadScene("GetItems");
                break;

            case "Zone_3":
                path.zone[6].SetActive(true);
                path.zone[5].SetActive(true);
                path.zone[2].SetActive(false);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_4":
                path.zone[7].SetActive(true);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_5":
                path.zone[7].SetActive(true);
                path.zone[6].SetActive(false);
                // SceneManager.LoadScene("Tetris");
                break;

            case "Zone_6":
                path.zone[8].SetActive(true);
                path.zone[5].SetActive(false);
                // SceneManager.LoadScene("Tetris_Elite");
                break;

            case "Zone_7":
                path.zone[9].SetActive(true);
                // SceneManager.LoadScene("Tetris_Elite");
                break;

            case "Zone_8":
                path.zone[7].SetActive(true);
                // SceneManager.LoadScene("GetItems");
                break;

            case "BossZone":
                // SceneManager.LoadScene("Tetris_Boss");
                break;
        }

        path.isMove = false;
    }



}

